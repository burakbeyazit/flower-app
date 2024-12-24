using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Application.Persistence;
using CicekApp.Domain.Entities;
using Dapper;
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
            var query = "SELECT * FROM public.users WHERE userid = @UserId";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
        }

        // Get a user by their email
        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM public.users WHERE email = @Email";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM public.users";
            return await _context.Database.GetDbConnection()
                .QueryAsync<User>(query);
        }

        // Add a new user
        public async Task AddAsync(User user)
        {
            var query = @"INSERT INTO public.users (email, passwordhash, firstname, lastname, phonenumber, createdat, lastonline, isactive, roleid, statusmessage, address, city, postalcode, country) 
                          VALUES(@Email, @PasswordHash, @FirstName, @LastName, @Phone, @CreatedAt, @LastOnline, @IsActive, @RoleId, @StatusMessage, @Address, @City, @PostalCode, @Country)";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Phone,
                user.CreatedAt,
                user.LastOnline,
                user.IsActive,
                user.RoleId,
                user.StatusMessage,
                user.Address,
                user.City,
                user.PostalCode,
                user.Country
            });
        }

        // Update an existing user
        public async Task UpdateAsync(User user)
        {
            var query = @"UPDATE public.users SET 
                          email = @Email, 
                          passwordhash = @PasswordHash, 
                          firstname = @FirstName, 
                          lastname = @LastName, 
                          phonenumber = @Phone, 
                          lastonline = @LastOnline, 
                          isactive = @IsActive, 
                          roleid = @RoleId, 
                          statusmessage = @StatusMessage, 
                          address = @Address, 
                          city = @City, 
                          postalcode = @PostalCode, 
                          country = @Country 
                          WHERE userid = @UserId";

            var parameters = new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Phone,
                user.LastOnline,
                user.IsActive,
                user.RoleId,
                user.StatusMessage,
                user.Address,
                user.City,
                user.PostalCode,
                user.Country,
                user.UserId
            };

            // Execute the query
            await _context.Database.GetDbConnection().ExecuteAsync(query, parameters);
        }

        // Delete a user
        public async Task DeleteAsync(int userId)
        {
            var query = "DELETE FROM public.users WHERE userid = @UserId";
            await _context.Database.GetDbConnection().ExecuteAsync(query, new { UserId = userId });
        }

        // Update the last online time for a user
        public async Task UpdateLastEntryDateAsync(int userId)
        {
            var query = "UPDATE public.users SET lastonline = @LastOnline WHERE userid = @UserId";
            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                LastOnline = DateTime.Now, // Set current time as the last online time
                UserId = userId
            });
        }

        // Update the role of a user
        public async Task UpdateRoleAsync(int userId, int newRoleId)
        {
            var query = "UPDATE public.users SET roleid = @RoleId WHERE userid = @UserId";
            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                RoleId = newRoleId,
                UserId = userId
            });
        }

        // Get all orders for a user
        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            var query = "SELECT * FROM public.orders WHERE userid = @UserId";
            return await _context.Database.GetDbConnection()
                .QueryAsync<Order>(query, new { UserId = userId });
        }

        public async Task SaveAddressAsync(AddressRequest addressRequest)
        {
            var query = @"UPDATE public.users SET 
                  address = @Address, 
                  city = @City, 
                  postalcode = @PostalCode, 
                  country = @Country 
                  WHERE email = @Username";

            var parameters = new
            {
                addressRequest.Address,
                addressRequest.City,
                addressRequest.PostalCode,
                addressRequest.Country,
                addressRequest.Username // Bu kısım email alanı olmalı
            };

            // Execute the query to update the user's address
            await _context.Database.GetDbConnection().ExecuteAsync(query, parameters);
        }


        // Update user and their orders (if needed)
        public async Task UpdateUserWithOrdersAsync(User user)
        {
            var query = @"UPDATE public.users SET 
                          email = @Email, 
                          passwordhash = @PasswordHash, 
                          firstname = @FirstName, 
                          lastname = @LastName, 
                          phonenumber = @Phone, 
                          lastonline = @LastOnline, 
                          isactive = @IsActive, 
                          roleid = @RoleId, 
                          statusmessage = @StatusMessage, 
                          address = @Address, 
                          city = @City, 
                          postalcode = @PostalCode, 
                          country = @Country 
                          WHERE userid = @UserId";

            var parameters = new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.Phone,
                user.LastOnline,
                user.IsActive,
                user.RoleId,
                user.StatusMessage,
                user.Address,
                user.City,
                user.PostalCode,
                user.Country,
                user.UserId
            };

            // Execute the query to update the user
            await _context.Database.GetDbConnection().ExecuteAsync(query, parameters);

            // If the user has orders, update them as well
            if (user.Orders != null && user.Orders.Any())
            {
                foreach (var order in user.Orders)
                {
                    var orderQuery = @"UPDATE public.orders SET 
                        totalprice = @TotalPrice,
                        status = @Status
                        WHERE orderid = @OrderId";

                    var orderParams = new
                    {
                        order.TotalPrice,
                        order.Status,
                        order.OrderId
                    };

                    await _context.Database.GetDbConnection().ExecuteAsync(orderQuery, orderParams);
                }
            }
        }
    }
}
