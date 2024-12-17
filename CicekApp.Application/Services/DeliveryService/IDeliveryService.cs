using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Services.DeliveryService
{
    public interface IDeliveryService
    {
        Task<Delivery> GetByIdAsync(int deliveryId);
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task DeleteAsync(int deliveryId);
    }
}