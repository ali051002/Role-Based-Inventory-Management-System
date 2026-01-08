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

namespace IMS.Infrastructure.Repository
{
    public class ProductRepository(DatabaseContext _dbContext, IMapper _mapper) : IProductRepository
    {
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
    }
}
