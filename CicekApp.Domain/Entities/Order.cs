using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Enums;

namespace CicekApp.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; } // Sipariş ID'si
        public int UserId { get; set; } // Kullanıcı ID'si
        public DateTime OrderDate { get; set; } // Sipariş Tarihi
        public decimal TotalPrice { get; set; } // Sipariş Toplam Fiyatı
        public OrderStatus Status { get; set; } // Sipariş Durumu

        public User User { get; set; } // Siparişe ait kullanıcı
        public ICollection<Flower> Flowers { get; set; } = new List<Flower>(); // Siparişle ilişkili ürünler
        public Delivery Delivery { get; set; } // Siparişin teslimatı
        public Cart Cart { get; set; } // Siparişle ilişkili sepet
        public int CartId { get; set; } // Sepet ID'si (Yeni eklenen alan)

    }

}