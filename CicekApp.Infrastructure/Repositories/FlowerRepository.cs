using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;
using CicekApp.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Infrastructure.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        private readonly AppDbContext _context;

        public FlowerRepository(AppDbContext context)
        {
            _context = context;
        }

        // FlowerId'ye göre çiçek getirir
        public async Task<Flower> GetByIdAsync(int flowerId)
        {
            var query = "SELECT * FROM Flowers WHERE FlowerId = @FlowerId";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<Flower>(query, new { FlowerId = flowerId });
        }

        // Tüm çiçekleri getirir
        public async Task<IEnumerable<Flower>> GetAllAsync()
        {
            var query = "SELECT * FROM Flowers";
            return await _context.Database.GetDbConnection()
                .QueryAsync<Flower>(query);
        }

        // Yeni bir çiçek ekler
        public async Task AddAsync(Flower flower)
        {
            var query = "INSERT INTO Flowers (FlowerName, FlowerType, Price, StockQuantity, Description) " +
                        "VALUES (@FlowerName, @FlowerType, @Price, @StockQuantity, @Description)";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                flower.FlowerName,
                flower.FlowerType,
                flower.Price,
                flower.StockQuantity,
                flower.Description
            });

        }

        // Var olan çiçeği günceller
        public async Task UpdateAsync(Flower flower)
        {
            var query = "UPDATE Flowers SET " +
                        "FlowerName = @FlowerName, " +
                        "FlowerType = @FlowerType, " +
                        "Price = @Price, " +
                        "StockQuantity = @StockQuantity, " +
                        "Description = @Description " +
                        "WHERE FlowerId = @FlowerId";



            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                flower.FlowerName,
                flower.FlowerType,
                flower.Price,
                flower.StockQuantity,
                flower.Description,
                flower.FlowerId
            });

        }

        // Çiçeği siler
        public async Task DeleteAsync(int flowerId)
        {
            var query = "DELETE FROM Flowers WHERE FlowerId = @FlowerId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new { FlowerId = flowerId });

        }
    }
}
