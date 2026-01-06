using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
