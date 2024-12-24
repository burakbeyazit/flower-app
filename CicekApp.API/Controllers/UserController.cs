using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CicekApp.Application.Models;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Services.Auth;
using CicekApp.Application.Services.UserService;
using CicekApp.Domain.Entities;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CicekApp.API.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;



        public UserController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerObject)
        {
            var response = await _authService.Register(registerObject);

            if (response.Success == false)
            {
                return BadRequest(response.Message);
            }
            else
            {
                return Ok(response.Message);

            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var response = await _authService.Login(model);

            if (response.Success == false)
            {
                return Unauthorized(response.Message);
            }

            else
            {
                return Ok(response);
            }

        }

        [HttpPost("saveAdress")]
        public async Task<IActionResult> SaveAdress(AddressRequest model)
        {

            await _userService.SaveAddressAsync(model);

            return Ok();


        }



    }
}