using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Services.FlowerService
{
    public class FlowerService : IFlowerService
    {
        private readonly AppDbContext _context;

        public FlowerService(AppDbContext context)
        {
            _context = context;
        }

        // Get a flower by its ID
        public async Task<Flower> GetByIdAsync(int flowerId)
        {
            var query = "SELECT * FROM public.flowers WHERE flowerid = @FlowerId";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<Flower>(query, new { FlowerId = flowerId });
        }

        // Get all flowers along with their categories
        public async Task<List<FlowerResponse>> GetAllAsync()
        {
            var query = @"
            SELECT 
                f.flowerid, 
                f.flowername, 
                f.price, 
                f.stockquantity, 
                f.description, 
                f.imageurl, 
                c.categoryid, 
                c.categoryname
            FROM 
                public.flowers f
            INNER JOIN 
                public.categories c ON f.categoryid = c.categoryid
            ORDER BY
                c.categoryid
            ";

            var result = await _context.Database.GetDbConnection()
                .QueryAsync<FlowerResponse>(query);
            return result.ToList();
        }

        // Add a new flower
        public async Task AddAsync(Flower flower)
        {
            var query = "INSERT INTO public.flowers (flowername, price, stockquantity, description, imageurl, categoryid) " +
                        "VALUES (@FlowerName, @Price, @StockQuantity, @Description, @ImageUrl, @CategoryId)";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                flower.FlowerName,
                flower.Price,
                flower.StockQuantity,
                flower.Description,
                flower.ImageUrl,
                flower.CategoryId
            });
        }

        // Update an existing flower
        public async Task UpdateAsync(Flower flower)
        {
            var query = "UPDATE public.flowers SET " +
                        "flowername = @FlowerName, " +
                        "price = @Price, " +
                        "stockquantity = @StockQuantity, " +
                        "description = @Description, " +
                        "imageurl = @ImageUrl, " +
                        "categoryid = @CategoryId " +
                        "WHERE flowerid = @FlowerId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                flower.FlowerName,
                flower.Price,
                flower.StockQuantity,
                flower.Description,
                flower.ImageUrl,
                flower.CategoryId,
                flower.FlowerId
            });
        }

        // Delete a flower
        public async Task DeleteAsync(int flowerId)
        {
            var query = "DELETE FROM public.flowers WHERE flowerid = @FlowerId";
            await _context.Database.GetDbConnection().ExecuteAsync(query, new { FlowerId = flowerId });
        }
    }
}
