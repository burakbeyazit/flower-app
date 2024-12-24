using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Request
{
    public class CreateOrderRequest
    {
        public string Username { get; set; }  // Kullanıcı adı
        public int CartId { get; set; }       // Sepet ID'si
    }

}