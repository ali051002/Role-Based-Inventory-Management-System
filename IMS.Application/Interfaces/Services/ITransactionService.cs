using IMS.Shared.DTOs.StockTransaction.Request;
using IMS.Shared.DTOs.StockTransaction.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<List<StockTransactionDetailResponseDto>> GetAllTransactions();
        Task StockTransaction(StockTransactionRequestDto request);

    }
}
