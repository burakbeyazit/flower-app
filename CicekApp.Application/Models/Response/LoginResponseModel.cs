using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Response
{
    public class LoginResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public JwtSecurityToken Token { get; set; }

    }
}