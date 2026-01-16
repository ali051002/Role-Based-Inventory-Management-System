using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Shared.DTOs.StockTransaction.Request
{
    public class StockTransactionRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string TransactionType { get; set; } = null!;
        public string? Reference { get; set; }
        public string CreatedBy { get; set; }
    }
}
