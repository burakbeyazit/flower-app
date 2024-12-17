using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Dapper;
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

            var query = "SELECT * FROM Deliveries WHERE DeliveryId = @DeliveryId";
            return await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Delivery>(query, new { DeliveryId = deliveryId });

        }

        // Tüm teslimatları getirir
        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {

            var query = "SELECT * FROM Deliveries";
            var deliveries = await _context.Database.GetDbConnection().QueryAsync<Delivery>(query);
            return deliveries;

        }

        // Yeni bir teslimat ekler
        public async Task AddAsync(Delivery delivery)
        {


            var query = "INSERT INTO Deliveries (OrderId, DeliveryDate, DeliveryAddress, DeliveryStatus, DeliveryPerson) " +
                        "VALUES (@OrderId, @DeliveryDate, @DeliveryAddress, @DeliveryStatus, @DeliveryPerson)";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                delivery.OrderId,
                delivery.DeliveryDate,
                delivery.DeliveryAddress,
                delivery.DeliveryStatus,
                delivery.DeliveryPerson
            });

        }

        // Var olan teslimatı günceller
        public async Task UpdateAsync(Delivery delivery)
        {

            var query = "UPDATE Deliveries SET " +
                        "OrderId = @OrderId, " +
                        "DeliveryDate = @DeliveryDate, " +
                        "DeliveryAddress = @DeliveryAddress, " +
                        "DeliveryStatus = @DeliveryStatus, " +
                        "DeliveryPerson = @DeliveryPerson " +
                        "WHERE DeliveryId = @DeliveryId";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                delivery.OrderId,
                delivery.DeliveryDate,
                delivery.DeliveryAddress,
                delivery.DeliveryStatus,
                delivery.DeliveryPerson,
                delivery.DeliveryId
            });

        }

        // Teslimatı siler
        public async Task DeleteAsync(int deliveryId)
        {

            var query = "DELETE FROM Deliveries WHERE DeliveryId = @DeliveryId";
            var result = await _context.Database.GetDbConnection().ExecuteAsync(query, new { DeliveryId = deliveryId });
        }

    }
}