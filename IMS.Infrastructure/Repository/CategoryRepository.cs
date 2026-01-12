using AutoMapper;
using Azure.Core;
using IMS.Application.Interfaces.Repositories;
using IMS.Domain.Entities;
using IMS.Infrastructure.DbContext;
using IMS.Shared.DTOs.Category.Request;
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

        public async Task<CategoryDetailResponseDto> GetCategoryById(Guid Id)
        {
            try
            {
                return _mapper.Map<CategoryDetailResponseDto>(await _dbContext.Categories.Where(p => p.Id == Id).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveCategory(SaveCategoryRequestDto request)
        {
            try
            {
                if (!request.Id.HasValue || request.Id.Value == Guid.Empty)
                {
                    Category newCategory = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Name,
                        IsActive = request.IsActive
                    };

                    await _dbContext.Categories.AddAsync(newCategory);
                    await _dbContext.SaveChangesAsync();

                    return;
                }

                var category = await _dbContext.Categories.FindAsync(request.Id.Value);

                category.Name = request.Name;
                category.IsActive = request.IsActive;


                _dbContext.Update(category);
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
