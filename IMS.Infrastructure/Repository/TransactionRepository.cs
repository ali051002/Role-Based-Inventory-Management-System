using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Domain.Entities;
using IMS.Infrastructure.DbContext;
using IMS.Shared.DTOs.Product.Response;
using IMS.Shared.DTOs.StockTransaction.Request;
using IMS.Shared.DTOs.StockTransaction.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Repository
{
    public class TransactionRepository(DatabaseContext _dbContext, IMapper _mapper) : ITransactionRepository
    {
        public async Task<List<StockTransactionDetailResponseDto>> GetAllTransactions()
        {
            try
            {
                var query = from s in _dbContext.StockTransactions
                            join p in _dbContext.Products on s.ProductId equals p.Id
                            select new StockTransactionDetailResponseDto
                            {
                                Id = s.Id,
                                ProductId = s.ProductId,
                                ProductName = p.Name,
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
