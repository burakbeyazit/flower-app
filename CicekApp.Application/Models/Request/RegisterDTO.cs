using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models
{
    public class RegisterDTO
    {
        public string Email { get; set; } // Kullanıcı e-posta adresi
        public string Password { get; set; } // Kullanıcı e-posta adresi

        public string FirstName { get; set; } // Ad
        public string LastName { get; set; } // Soyad
        public string PhoneNumber { get; set; } // Telefon numarası
        public string StatusMessage { get; set; } // Kullanıcı durumu (opsiyonel, ör. "Çevrimiçi", "Meşgul")

    }
}