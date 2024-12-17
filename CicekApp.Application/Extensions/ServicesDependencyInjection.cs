using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Services.Auth;
using CicekApp.Application.Services.CustomerService;
using CicekApp.Application.Services.DeliveryService;
using CicekApp.Application.Services.FlowerService;
using CicekApp.Application.Services.UserService;
using CicekApp.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CicekApp.Application.Extensions
{
    public static class ServicesDependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Repository ve Servisleri buraya ekleyin
            services.AddScoped<IAuthService, AuthService>(); // Servis kaydı
            services.AddScoped<IFlowerService, FlowerService>(); // Servis kaydı
            services.AddScoped<IDeliveryService, DeliveryService>(); // Servis kaydı
            services.AddScoped<IUserService, UserService>(); // Servis kaydı
            services.AddScoped<ICustomerService, CustomerService>(); // Servis kaydı


            // Diğer servisler ve repository'ler...
        }
    }
}