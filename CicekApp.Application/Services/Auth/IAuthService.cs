using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models;
using CicekApp.Application.Models.Response;

namespace CicekApp.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseModel> Login(LoginModel loginModel);

        Task<RegisterResponseModel> Register(RegisterDTO registerObject);
    }
}