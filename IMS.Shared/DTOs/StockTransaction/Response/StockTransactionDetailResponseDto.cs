using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Shared.DTOs.StockTransaction.Response
{
    public class StockTransactionDetailResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string TransactionType { get; set; } = null!;
    }
}
