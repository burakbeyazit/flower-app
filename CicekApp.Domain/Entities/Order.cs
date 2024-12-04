using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        // Navigation Property
        public virtual Customer Customer { get; set; }

        // Her siparişin sadece bir teslimatı vardır
        public virtual Delivery Delivery { get; set; }  // Bu, bir siparişin bir teslimatını ifade eder
    }



}