using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Request
{
    public class AddressRequest
    {
        public string Username { get; set; } // Müşteri adresi

        public string Address { get; set; } // Müşteri adresi
        public string City { get; set; } // Müşteri şehri
        public string PostalCode { get; set; } // Müşteri posta kodu
        public string Country { get; set; } // Müşteri ülke
    }
}