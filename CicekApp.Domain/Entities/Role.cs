using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekApp.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>(); // Rol ile ilişkili kullanıcılar
    }

}