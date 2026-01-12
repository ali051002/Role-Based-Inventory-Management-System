using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.Category.Request;
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

        public Task<CategoryDetailResponseDto> GetCategoryById(Guid Id)
        {
            try
            {
                return _iCategoryRepository.GetCategoryById(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task SaveCategory(SaveCategoryResponseDto request)
        {
            try
            {
                return _iCategoryRepository.SaveCategory(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
