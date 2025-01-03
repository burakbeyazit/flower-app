using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Flower
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; } // Kategorisi
        public Category Category { get; set; } // Kategori ile ilişki

        public ICollection<CartFlowers> CartFlowers { get; set; }  // Cart'ı çiçeklerle ilişkilendiren koleksiyon

    }

}