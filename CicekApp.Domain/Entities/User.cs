using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; } // Kullanıcının benzersiz kimliği
        public string Email { get; set; } // Kullanıcı e-posta adresi
        public string PasswordHash { get; set; } // Şifre hash'i
        public string FirstName { get; set; } // Ad
        public string LastName { get; set; } // Soyad
        public string Phone { get; set; } // Telefon numarası
        public DateTime CreatedAt { get; set; } // Hesap oluşturulma tarihi
        public DateTime? LastOnline { get; set; } // Son çevrimiçi olma tarihi (nullable)
        public bool IsActive { get; set; } // Kullanıcı aktif mi?
        public int RoleId { get; set; } // Kullanıcı rolü (örneğin Admin, User)
        public string StatusMessage { get; set; } // Kullanıcı durumu (opsiyonel, ör. "Çevrimiçi", "Meşgul")

        public Role Role { get; set; } // Kullanıcıya ait rol

        public ICollection<Order> Orders { get; set; } = new List<Order>(); // Kullanıcıya ait siparişler
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        // Müşteri bilgileri
        public string Address { get; set; } // Müşteri adresi
        public string City { get; set; } // Müşteri şehri
        public string PostalCode { get; set; } // Müşteri posta kodu
        public string Country { get; set; } // Müşteri ülke
    }

}