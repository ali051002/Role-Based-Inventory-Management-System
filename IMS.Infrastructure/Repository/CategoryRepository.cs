using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Infrastructure.DbContext;
using IMS.Shared.DTOs.Category.Response;
using IMS.Shared.DTOs.Product.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Repository
{
    public class CategoryRepository(DatabaseContext _dbContext, IMapper _mapper) : ICategoryRepository
    {
        public async Task<List<CategoryDetailResponseDto>> GetAllCategories()
        {
            try
            {
                return await _dbContext.CategoryResponseDto.FromSql($"EXEC dbo.sp_GetAllCategories").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
