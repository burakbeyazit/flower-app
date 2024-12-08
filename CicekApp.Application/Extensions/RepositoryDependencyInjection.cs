using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Infrastructure.Persistence;
using CicekApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CicekApp.Application.Extensions
{
    public static class RepositoryDependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>(); // Repository kaydı
            services.AddScoped<ICustomerRepository, CustomerRepository>(); // Repository kaydı
            services.AddScoped<IDeliveryRepository, DeliveryRepository>(); // Repository kaydı
            services.AddScoped<IFlowerRepository, FlowerRepository>(); // Repository kaydı



        }
    }
}