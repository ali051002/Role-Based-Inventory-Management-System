using IMS.Shared.DTOs.Category.Request;
using IMS.Shared.DTOs.Category.Response;
using IMS.Shared.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDetailResponseDto>> GetAllCategories();
        Task<CategoryDetailResponseDto> GetCategoryById(Guid Id);
        Task SaveCategory(SaveCategoryResponseDto request);
    }
}
