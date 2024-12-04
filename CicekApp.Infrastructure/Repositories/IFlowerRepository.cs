using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;

namespace CicekApp.Infrastructure.Repositories
{
    public interface IFlowerRepository
    {
        Task<Flower> GetByIdAsync(int flowerId);
        Task<IEnumerable<Flower>> GetAllAsync();
        Task AddAsync(Flower flower);
        Task UpdateAsync(Flower flower);
        Task DeleteAsync(int flowerId);
    }
}