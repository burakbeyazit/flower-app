using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Application.Models.Request;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Services.UserService
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int userId);

        // Kullanıcı Emailine göre kullanıcıyı getirir
        Task<User> GetByEmailAsync(string email);

        // Tüm kullanıcıları getirir
        Task<IEnumerable<User>> GetAllAsync();

        // Yeni bir kullanıcı ekler
        Task AddAsync(User user);

        // Var olan bir kullanıcıyı günceller
        Task UpdateAsync(User user);

        // Kullanıcıyı siler
        Task DeleteAsync(int userId);

        Task UpdateLastEntryDateAsync(int userId);
        Task SaveAddressAsync(AddressRequest addressRequest);
    }
}