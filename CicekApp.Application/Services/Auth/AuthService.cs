using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CicekApp.Application.Models;
using CicekApp.Application.Models.Response;
using CicekApp.Domain.Entities;
using CicekApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CicekApp.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userManager;

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IUserRepository userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<RegisterResponseModel> Register(RegisterDTO registerObject)
        {
            if (await _userManager.GetByEmailAsync(registerObject.Email) != null)
            {
                return new RegisterResponseModel
                {
                    Message = "Kullanıcı Zaten Sisteme Kayıtlıdır..",
                    Success = false
                };
            }

            var user = new User
            {
                Email = registerObject.Email,
                FirstName = registerObject.FirstName,
                IsActive = true,
                CreatedAt = DateTime.Now,
                LastName = registerObject.LastName,
                LastOnline = DateTime.Now,
                PhoneNumber = registerObject.PhoneNumber,
                RoleId = 3, // 1-Admin 2-Eleman 3-Normal üye
                StatusMessage = registerObject.StatusMessage ?? "",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerObject.Password)
            };

            await _userManager.AddAsync(user);

            return new RegisterResponseModel { Message = "Kullanıcı Başarıyla Eklenmiştir", Success = true };

        }

        public async Task<LoginResponseModel> Login(LoginModel loginModel)
        {

            var user = await _userManager.GetByEmailAsync(loginModel.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, user.PasswordHash))
            {
                return new LoginResponseModel { Message = "Şifre Hatalı...", Success = false };
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            await _userManager.UpdateLastEntryDateAsync(user.UserId);
            return new LoginResponseModel { Message = "Kullanıcı Başarıyla Eklenmiştir", Success = true, Token = token };

        }

    }
}