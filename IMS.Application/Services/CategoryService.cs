using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.Category.Response;
using IMS.Shared.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class CategoryService(ICategoryRepository _iCategoryRepository, IMapper _mapper) : ICategoryService
    {
        public async Task<List<CategoryDetailResponseDto>> GetAllCategories()
        {
            try
            {
                return await _iCategoryRepository.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
