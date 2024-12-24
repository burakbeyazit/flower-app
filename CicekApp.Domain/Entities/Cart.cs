using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; } // Sepet ID
        public decimal TotalAmount { get; set; } // Sepet Toplam Tutarı
        public DateTime CreatedDate { get; set; } // Sepet Oluşturulma Tarihi
        public int UserId { get; set; } // Sepet sahibinin kullanıcı ID'si

        public ICollection<Flower> Flowers { get; set; } = new List<Flower>(); // Sepetle ilişkili ürünler
        public User User { get; set; } // Sepet sahibinin kullanıcı bilgisi
        public int? OrderId { get; set; } // Sipariş ID'si (null olabilir)
        public Order Order { get; set; } // Sepetle ilişkili sipariş
        public ICollection<CartFlowers> CartFlowers { get; set; }  // Cart'ı çiçeklerle ilişkilendiren koleksiyon

    }

}