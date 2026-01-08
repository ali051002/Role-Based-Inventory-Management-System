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
        public decimal UnitPrice { get; set; }
        public int CurrentStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public CategoryResponseDto Category { get; set; } = null!;
        public ICollection<StockTransactionResponseDto> StockTransactions { get; set; } = new List<StockTransactionResponseDto>();
    }

    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class StockTransactionResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = null!;
    }
}
