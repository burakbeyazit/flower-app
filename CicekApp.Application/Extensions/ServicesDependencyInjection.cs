using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace CicekApp.Application.Extensions
{
    public static class ServicesDependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Repository ve Servisleri buraya ekleyin
            services.AddScoped<IAuthService, AuthService>(); // Servis kaydı
            // Diğer servisler ve repository'ler...
        }
    }
}