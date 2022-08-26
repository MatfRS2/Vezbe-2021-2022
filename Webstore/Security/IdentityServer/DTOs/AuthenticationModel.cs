using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.DTOs
{
    public class AuthenticationModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
