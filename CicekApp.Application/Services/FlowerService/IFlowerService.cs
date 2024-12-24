using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response.FlowerResponse;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Services.FlowerService
{
    public interface IFlowerService
    {
        Task<Flower> GetByIdAsync(int flowerId);
        Task<List<FlowerResponse>> GetAllAsync();
        Task AddAsync(Flower flower);

        Task UpdateAsync(Flower flower);

        Task DeleteAsync(int flowerId);

    }
}