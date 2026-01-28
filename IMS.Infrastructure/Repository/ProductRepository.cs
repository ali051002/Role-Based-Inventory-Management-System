using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Domain.Entities;
using IMS.Infrastructure.DbContext;
using IMS.Shared.DTOs.Product.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Shared.DTOs.Product.Request;

namespace IMS.Infrastructure.Repository
{
    public class ProductRepository(DatabaseContext _dbContext, IMapper _mapper) : IProductRepository
    {
        public async Task DeleteProduct(Guid Id)
        {
            try
            {
                var product = _dbContext.Products.Where(p => p.Id == Id).FirstOrDefault();
                if (product != null)
                {
                    _dbContext.Products.Remove(product);
                    await _dbContext.SaveChangesAsync();

                    return;
                }
            }
            catch( Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductDetailResponseDto>> GetAllProducts()
        {
            try
            {
                return await _dbContext.ProductDetailResponseDto.FromSql($"EXEC dbo.sp_GetAllProductsWithCategory").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductDetailResponseDto> GetProductById(Guid Id)
        {
            try
            {
                return _mapper.Map<ProductDetailResponseDto>(await _dbContext.Products.Where(p => p.Id == Id).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductDetailResponseDto>> GetProductsByCategory(Guid Id)
        {
            try
            {
                return await _dbContext.ProductDetailResponseDto.FromSqlRaw("EXEC dbo.sp_GetProductsByCategoryId @CategoryId = {0}", Id).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveProduct(SaveProductRequestDto request)
        {
            try
            {
                if (!request.Id.HasValue || request.Id.Value == Guid.Empty)
                {
                    Product newProduct = new Product
                    {
                        Id = Guid.NewGuid(),
                        ProductCode = request.ProductCode,
                        Name = request.Name,
                        CategoryId = request.CategoryId,
                        UnitPrice = request.UnitPrice,
                        CurrentStock = request.CurrentStock,
                        IsActive = request.IsActive,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _dbContext.Products.AddAsync(newProduct);
                    await _dbContext.SaveChangesAsync();

                    return;
                }

                var product = await _dbContext.Products.FindAsync(request.Id.Value);

                product.ProductCode = request.ProductCode;
                product.Name = request.Name;
                product.CategoryId = request.CategoryId;
                product.UnitPrice = request.UnitPrice;
                product.CurrentStock = request.CurrentStock;
                product.IsActive = request.IsActive;
                product.CreatedDate = DateTime.UtcNow;


                _dbContext.Update(product);
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
