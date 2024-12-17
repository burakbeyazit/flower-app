using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Services.FlowerService
{
    public interface IFlowerService
    {
        Task<Flower> GetByIdAsync(int flowerId);

    }
}