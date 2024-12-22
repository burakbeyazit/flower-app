using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Flower> Flowers { get; set; }
    }

}