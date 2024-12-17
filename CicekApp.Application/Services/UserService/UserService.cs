using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // Belirli bir kullanıcıyı ID ile getirir
        public async Task<User> GetByIdAsync(int userId)
        {
            var query = "SELECT * FROM Users WHERE UserId = @UserId";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
        }


        // Belirli bir kullanıcıyı Email ile getirir

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM Users WHERE Email = @Email";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        // Tüm kullanıcıları getirir
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";
            return await _context.Database.GetDbConnection()
                .QueryAsync<User>(query);
        }

        // Yeni bir kullanıcı ekler
        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO Users (Email, PasswordHash, FirstName, LastName, RoleId, CreatedAt, LastOnline) " +
                        "VALUES (@Email, @PasswordHash, @FirstName, @LastName, @RoleId, @CreatedAt, @LastOnline)";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.RoleId,
                user.CreatedAt,
                user.LastOnline
            });
        }

        // Var olan bir kullanıcıyı günceller
        public async Task UpdateAsync(User user)
        {
            var query = "UPDATE Users SET " +
                        "Email = @Email, " +
                        "PasswordHash = @PasswordHash, " +
                        "FirstName = @FirstName, " +
                        "LastName = @LastName, " +
                        "RoleId = @RoleId, " +
                        "LastOnline = @LastOnline " +
                        "WHERE UserId = @UserId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.RoleId,
                user.LastOnline,
                user.UserId
            });
        }

        // Kullanıcıyı siler
        public async Task DeleteAsync(int userId)
        {
            var query = "DELETE FROM Users WHERE UserId = @UserId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new { UserId = userId });
        }

        //Kullanıcının son login saatini günceller


        public async Task UpdateLastEntryDateAsync(int userId)
        {
            var query = "UPDATE Users SET LastOnline = @LastOnline WHERE UserId = @UserId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                LastOnline = DateTime.Now, // Şu anki tarihi son giriş tarihi olarak ayarlıyoruz
                UserId = userId
            });
        }
    }
}