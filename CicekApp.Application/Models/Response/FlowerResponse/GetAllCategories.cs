using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Application.Models.Response.FlowerResponse
{
    public class GetAllCategories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}