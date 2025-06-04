using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
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
            return await _context.Flowers
                .FirstOrDefaultAsync(f => f.FlowerId == flowerId);
        }

        // Get all flowers along with their categories
        public async Task<List<FlowerResponse>> GetAllAsync()
        {
            return await _context.Flowers
                .OrderBy(f => f.CategoryId)
                .Select(f => new FlowerResponse
                {
                    FlowerId = f.FlowerId,
                    FlowerName = f.FlowerName,
                    Price = f.Price,
                    StockQuantity = f.StockQuantity,
                    Description = f.Description,
                    ImageUrl = f.ImageUrl,
                    CategoryId = f.CategoryId,
                    Category = f.Category.CategoryName
                })
                .ToListAsync();
        }

        // Add a new flower
        public async Task AddAsync(Flower flower)
        {
            await _context.Flowers.AddAsync(flower);
            await _context.SaveChangesAsync();
        }

        // Update an existing flower
        public async Task UpdateAsync(Flower flower)
        {
            var dbFlower = await _context.Flowers.FirstOrDefaultAsync(f => f.FlowerId == flower.FlowerId);
            if (dbFlower != null)
            {
                dbFlower.FlowerName = flower.FlowerName;
                dbFlower.Price = flower.Price;
                dbFlower.StockQuantity = flower.StockQuantity;
                dbFlower.Description = flower.Description;
                dbFlower.ImageUrl = flower.ImageUrl;
                dbFlower.CategoryId = flower.CategoryId;
                await _context.SaveChangesAsync();
            }
        }

        // Delete a flower
        public async Task DeleteAsync(int flowerId)
        {
            var flower = await _context.Flowers.FirstOrDefaultAsync(f => f.FlowerId == flowerId);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
                await _context.SaveChangesAsync();
            }
        }
    }
}
