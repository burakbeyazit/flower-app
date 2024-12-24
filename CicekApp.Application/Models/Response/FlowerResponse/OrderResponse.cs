using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Enums;

namespace CicekApp.Application.Models.Response.FlowerResponse
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public int CartId { get; set; }
        public string Username { get; set; }
        public List<FlowerResponse> Flowers { get; set; } = new List<FlowerResponse>();
    }
}