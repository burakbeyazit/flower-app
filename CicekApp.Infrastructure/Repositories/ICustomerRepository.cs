using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;

namespace CicekApp.Infrastructure.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int customerId);  // Dönüş tipi Customer
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int customerId);
    }

}