using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllCategories> GetByIdAsync(int categoryid)
        {
            // EF ile LINQ üzerinden Category çekip GetAllCategories DTO'ya mapliyoruz
            return await _context.Categories
                .Where(c => c.CategoryId == categoryid)
                .Select(c => new GetAllCategories
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    // Buraya başka field ekleyebilirsin
                })
                .FirstOrDefaultAsync();
        }

        // Tüm kategorileri getirir
        public async Task<List<GetAllCategories>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new GetAllCategories
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    // Ekstra alanlar eklenecekse ekle
                })
                .ToListAsync();
        }
    }
}
