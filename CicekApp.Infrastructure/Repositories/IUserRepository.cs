using System.Collections.Generic;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;

namespace CicekApp.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        // Kullanıcı ID'sine göre kullanıcıyı getirir
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
    }
}
