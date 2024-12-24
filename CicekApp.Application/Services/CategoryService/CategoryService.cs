using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Dapper;
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

            var query = "SELECT * FROM categories WHERE categoryid = @categoryid";
            return await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<GetAllCategories>(query, new { DeliveryId = categoryid });

        }

        // Tüm teslimatları getirir
        public async Task<List<GetAllCategories>> GetAllAsync()
        {

            var query = "SELECT * FROM categories";
            var categories = await _context.Database.GetDbConnection().QueryAsync<GetAllCategories>(query);
            return categories.ToList();

        }
    }
}