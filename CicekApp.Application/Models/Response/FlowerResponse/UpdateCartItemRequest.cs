using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Response.FlowerResponse
{
    public class UpdateCartItemRequest
    {
        public int FlowerId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
    }
}