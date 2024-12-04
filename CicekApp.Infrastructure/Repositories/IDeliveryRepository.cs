using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;

namespace CicekApp.Infrastructure.Repositories
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetByIdAsync(int deliveryId);
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task DeleteAsync(int deliveryId);
    }
}