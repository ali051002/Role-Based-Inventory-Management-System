using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
    }
}
