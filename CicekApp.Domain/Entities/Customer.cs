using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Navigation Property (bir müşteri birden fazla siparişe sahip olabilir)
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}