using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CicekApp.Application.Models;
using CicekApp.Application.Models.Response;
using CicekApp.Application.Services.UserService;
using CicekApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CicekApp.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userManager;

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, IUserService userManager)
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
                Phone = registerObject.PhoneNumber,
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

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.FirstName),        // İsim
        new Claim(ClaimTypes.Surname, user.LastName),  // Soyisim
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.RoleId.ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(12),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            // JWT'yi string olarak döndürmek
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            await _userManager.UpdateLastEntryDateAsync(user.UserId);

            return new LoginResponseModel { Message = "Giriş Başarılıdır...", Success = true, Token = tokenString };
        }


    }
}