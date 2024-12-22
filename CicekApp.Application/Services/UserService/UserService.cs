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
            var query = "SELECT * FROM Users WHERE userid = @UserId";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<User>(query, new { UserId = userId });
        }


        // Belirli bir kullanıcıyı Email ile getirir

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM public.users  WHERE email = @Email";
            return await _context.Database.GetDbConnection()
                .QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        // Tüm kullanıcıları getirir
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM public.users ";
            return await _context.Database.GetDbConnection()
                .QueryAsync<User>(query);
        }

        // Yeni bir kullanıcı ekler
        public async Task AddAsync(User user)
        {
            var query = "INSERT INTO public.users (email, passwordhash, firstname, lastname, phonenumber, createdat, lastonline, isactive, roleid, statusmessage) " +
            "VALUES(@Email, @PasswordHash, @FirstName, @LastName, @PhoneNumber, @CreatedAt, @LastOnline, @IsActive, @RoleId, @StatusMessage)";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.CreatedAt,
                user.LastOnline,
                user.IsActive,
                user.RoleId,
                user.StatusMessage
            });
        }

        public async Task UpdateAsync(User user)
        {
            var query = @"UPDATE public.users SET 
                  email = @Email, 
                  passwordhash = @PasswordHash, 
                  firstname = @FirstName, 
                  lastname = @LastName, 
                  phonenumber = @PhoneNumber, 
                  lastonline = @LastOnline, 
                  isactive = @IsActive, 
                  roleid = @RoleId, 
                  statusmessage = @StatusMessage
                  WHERE userid = @UserId";

            var parameters = new
            {
                user.Email,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.LastOnline,
                user.IsActive,
                user.RoleId,
                user.StatusMessage,
                user.UserId
            };

            // Execute the query
            await _context.Database.GetDbConnection().ExecuteAsync(query, parameters);
        }



        // Kullanıcıyı siler
        public async Task DeleteAsync(int userId)
        {
            var query = "DELETE FROM public.users  WHERE userid = @UserId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new { UserId = userId });
        }

        //Kullanıcının son login saatini günceller


        public async Task UpdateLastEntryDateAsync(int userId)
        {
            var query = "UPDATE public.users  SET lastonline = @LastOnline WHERE userid = @UserId";

            await _context.Database.GetDbConnection().ExecuteAsync(query, new
            {
                LastOnline = DateTime.Now, // Şu anki tarihi son giriş tarihi olarak ayarlıyoruz
                UserId = userId
            });
        }
    }
}