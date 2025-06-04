using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Services.DeliveryService
{
    public class DeliveryService : IDeliveryService
    {
        private readonly AppDbContext _context;

        public DeliveryService(AppDbContext context)
        {
            _context = context;
        }

        // DeliveryId'ye göre teslimat getirir
        public async Task<Delivery> GetByIdAsync(int deliveryId)
        {
            return await _context.Deliveries
                .FirstOrDefaultAsync(d => d.DeliveryId == deliveryId);
        }

        // Tüm teslimatları getirir
        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        // Yeni bir teslimat ekler
        public async Task AddAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        // Var olan teslimatı günceller
        public async Task UpdateAsync(Delivery delivery)
        {
            var dbDelivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.DeliveryId == delivery.DeliveryId);
            if (dbDelivery != null)
            {
                dbDelivery.OrderId = delivery.OrderId;
                dbDelivery.DeliveryDate = delivery.DeliveryDate;
                dbDelivery.DeliveryAddress = delivery.DeliveryAddress;
                dbDelivery.DeliveryStatus = delivery.DeliveryStatus;
                dbDelivery.DeliveryPerson = delivery.DeliveryPerson;
                await _context.SaveChangesAsync();
            }
        }

        // Teslimatı siler
        public async Task DeleteAsync(int deliveryId)
        {
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.DeliveryId == deliveryId);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
    }
}
