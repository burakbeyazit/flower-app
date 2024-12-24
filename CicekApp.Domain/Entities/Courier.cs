using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Courier
    {
        public int CourierId { get; set; } // Kurye ID'si
        public string CourierName { get; set; } // Kurye Adı
        public string CourierPhone { get; set; } // Kurye Telefonu

        public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>(); // Kurye ile ilişkili teslimatlar
    }


}