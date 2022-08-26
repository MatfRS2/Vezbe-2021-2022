using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDetails>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDetails>>(users));
        }

        [Authorize(Roles = "Administrator,Buyer")]
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(UserDetails), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDetails>> GetUser(string username)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);

            return Ok(_mapper.Map<UserDetails>(user));
        }
    }
}
