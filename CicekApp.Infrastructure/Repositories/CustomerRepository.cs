using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;
using CicekApp.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var query = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";
            return await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<Customer>(query, new { CustomerId = customerId });
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var query = "SELECT * FROM Customers";
            return await _context.Database.GetDbConnection().QueryAsync<Customer>(query);
        }

        public async Task AddAsync(Customer customer)
        {
            var query = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) " +
                        "VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";
            await _context.Database.GetDbConnection().ExecuteAsync(query, customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            var query = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, " +
                        "Email = @Email, Phone = @Phone, Address = @Address WHERE CustomerId = @CustomerId";
            await _context.Database.GetDbConnection().ExecuteAsync(query, customer);
        }

        public async Task DeleteAsync(int customerId)
        {
            var query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
            await _context.Database.GetDbConnection().ExecuteAsync(query, new { CustomerId = customerId });
        }
    }
}