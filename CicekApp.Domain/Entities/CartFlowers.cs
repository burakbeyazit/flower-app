using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{

    public class CartFlowers
    {
        public int CartId { get; set; }  // Sepet ID'si
        public int FlowerId { get; set; }  // Çiçek ID'si
        public int Quantity { get; set; }  // Çiçek miktarı

        // Navigation properties
        public Cart Cart { get; set; }  // Cart entity'si ile ilişkisi
        public Flower Flower { get; set; }  // Flower entity'si ile ilişkisi
    }

}