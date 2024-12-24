using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Delivery
    {
        public int DeliveryId { get; set; } // Teslimat ID'si
        public int OrderId { get; set; } // Sipariş ID'si
        public DateTime DeliveryDate { get; set; } // Teslimat Tarihi
        public string DeliveryAddress { get; set; } // Teslimat Adresi
        public string DeliveryStatus { get; set; } // Teslimat Durumu
        public string DeliveryPerson { get; set; } // Teslimatı yapan kişi

        public int CourierId { get; set; } // Kurye ID'si
        public Courier Courier { get; set; } // Kurye ile ilişki

        public Order Order { get; set; } // Teslimat bir siparişle ilişkilidir
    }



}