using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Shared.DTOs.Product.Response
{
    public class ProductDetailResponseDto
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
