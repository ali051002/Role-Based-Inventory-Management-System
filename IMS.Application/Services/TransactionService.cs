using IMS.Application.Interfaces.Repositories;
using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.StockTransaction.Request;
using IMS.Shared.DTOs.StockTransaction.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class TransactionService(ITransactionRepository _iTransactionRepository) : ITransactionService
    {
        public async Task DeleteTransaction(List<StockTransactionRequestDto> request)
        {
            try
            {
                await _iTransactionRepository.DeleteTransaction(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StockTransactionDetailResponseDto>> GetAllTransactions()
        {
            try
            {
                return await _iTransactionRepository.GetAllTransactions();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task StockTransaction(StockTransactionRequestDto request)
        {
            try
            {
                await _iTransactionRepository.StockTransaction(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
