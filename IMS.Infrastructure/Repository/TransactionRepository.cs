using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Domain.Entities;
using IMS.Infrastructure.DbContext;
using IMS.Shared.DTOs.StockTransaction.Request;
using IMS.Shared.DTOs.StockTransaction.Response;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Repository
{
    public class TransactionRepository(DatabaseContext _dbContext, IMapper _mapper) : ITransactionRepository
    {
        public async Task DeleteTransaction(List<StockTransactionRequestDto> request)
        {
            try
            {
                var idsToDelete = request.Select(r => r.Id).ToList();

                await _dbContext.StockTransactions.Where(st => idsToDelete.Contains(st.Id)).ExecuteDeleteAsync();
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
                var query = from s in _dbContext.StockTransactions
                            select new StockTransactionDetailResponseDto
                            {
                                Id = s.Id,
                                ProductId = s.ProductId,
                                ProductName = s.ProductName,
                                Quantity = s.Quantity,
                                CreatedDate = s.CreatedDate,
                                CreatedBy = s.CreatedBy,
                                TransactionType = s.TransactionType
                            };
                return await query.AsNoTracking().ToListAsync();
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
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);

                if (request.TransactionType == "IN")
                {
                    product.CurrentStock += request.Quantity;
                }
                else if (request.TransactionType == "OUT")
                {
                    if (product.CurrentStock < request.Quantity)
                    {
                        throw new Exception("Insufficient stock for the product.");
                    }
                    product.CurrentStock -= request.Quantity;
                }

                _dbContext.Products.Update(product);

                StockTransaction newTransaction = new StockTransaction
                {
                    Id = Guid.NewGuid(),
                    ProductId = request.ProductId,
                    ProductName = product.Name,
                    Quantity = request.Quantity,
                    TransactionType = request.TransactionType,
                    Reference = request.Reference,
                    CreatedBy = request.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };

                await _dbContext.StockTransactions.AddAsync(newTransaction);

                await _dbContext.SaveChangesAsync();

                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
