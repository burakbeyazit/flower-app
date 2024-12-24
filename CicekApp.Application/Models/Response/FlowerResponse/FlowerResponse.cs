using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Response.FlowerResponse
{
    public class FlowerResponse
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }

    }
}