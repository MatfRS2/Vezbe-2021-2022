using IdentityServer.Data;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _dbContext;

        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration, ApplicationContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<User> ValidateUser(UserCredentialsDto userCredentials)
        {
            var user = await _userManager.FindByNameAsync(userCredentials.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userCredentials.Password))
            {
                return null;
            }
            return user;
        }

        public async Task<AuthenticationModel> CreateAuthenticationModel(User user)
        {
            var accessToken = await CreateAccessToken(user);
            var refreshToken = await CreateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new AuthenticationModel { AccessToken = accessToken, RefreshToken = refreshToken.Token };
        }

        public async Task RemoveRefreshToken(User user, string refreshToken)
        {
            user.RefreshTokens.RemoveAll(r => r.Token == refreshToken);
            await _userManager.UpdateAsync(user);

            var token = _dbContext.RefreshTokens.FirstOrDefault(r => r.Token == refreshToken);
            if (token == null)
            {
                return;
            }

            _dbContext.RefreshTokens.Remove(token);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<string> CreateAccessToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var token = GenerateToken(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:secretKey"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var token = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );

            return token;
        }

        private async Task<RefreshToken> CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var token = new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_configuration.GetValue<string>("RefreshTokenExpires")))
            };

            _dbContext.RefreshTokens.Add(token);
            await _dbContext.SaveChangesAsync();

            return token;
        }
    }
}
