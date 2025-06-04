using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Get a user by their ID
        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // Get a user by their email
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        // Add a new user
        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Update an existing user
        public async Task UpdateAsync(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
            if (dbUser == null) return;

            dbUser.Email = user.Email;
            dbUser.PasswordHash = user.PasswordHash;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Phone = user.Phone;
            dbUser.LastOnline = user.LastOnline;
            dbUser.IsActive = user.IsActive;
            dbUser.RoleId = user.RoleId;
            dbUser.StatusMessage = user.StatusMessage;
            dbUser.Address = user.Address;
            dbUser.City = user.City;
            dbUser.PostalCode = user.PostalCode;
            dbUser.Country = user.Country;

            await _context.SaveChangesAsync();
        }

        // Delete a user
        public async Task DeleteAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Update the last online time for a user
        public async Task UpdateLastEntryDateAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.LastOnline = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        // Update the role of a user
        public async Task UpdateRoleAsync(int userId, int newRoleId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.RoleId = newRoleId;
                await _context.SaveChangesAsync();
            }
        }

        // Get all orders for a user
        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        // Update user address info by email
        public async Task SaveAddressAsync(AddressRequest addressRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == addressRequest.Username);
            if (user != null)
            {
                user.Address = addressRequest.Address;
                user.City = addressRequest.City;
                user.PostalCode = addressRequest.PostalCode;
                user.Country = addressRequest.Country;
                await _context.SaveChangesAsync();
            }
        }

        // Update user and their orders (if needed)
        public async Task UpdateUserWithOrdersAsync(User user)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if (dbUser == null) return;

            dbUser.Email = user.Email;
            dbUser.PasswordHash = user.PasswordHash;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Phone = user.Phone;
            dbUser.LastOnline = user.LastOnline;
            dbUser.IsActive = user.IsActive;
            dbUser.RoleId = user.RoleId;
            dbUser.StatusMessage = user.StatusMessage;
            dbUser.Address = user.Address;
            dbUser.City = user.City;
            dbUser.PostalCode = user.PostalCode;
            dbUser.Country = user.Country;

            // Eğer order güncellemesi gerekiyorsa
            if (user.Orders != null && user.Orders.Any())
            {
                foreach (var updatedOrder in user.Orders)
                {
                    var dbOrder = dbUser.Orders.FirstOrDefault(o => o.OrderId == updatedOrder.OrderId);
                    if (dbOrder != null)
                    {
                        dbOrder.TotalPrice = updatedOrder.TotalPrice;
                        dbOrder.Status = updatedOrder.Status;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
