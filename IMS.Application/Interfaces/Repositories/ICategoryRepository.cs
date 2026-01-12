using IMS.Shared.DTOs.Category.Request;
using IMS.Shared.DTOs.Category.Response;
using IMS.Shared.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDetailResponseDto>> GetAllCategories();
        Task<CategoryDetailResponseDto> GetCategoryById(Guid Id);
        Task SaveCategory(SaveCategoryRequestDto request);
    }
}
