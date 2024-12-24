using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<GetAllCategories> GetByIdAsync(int categoryid);
        Task<List<GetAllCategories>> GetAllAsync();
    }
}