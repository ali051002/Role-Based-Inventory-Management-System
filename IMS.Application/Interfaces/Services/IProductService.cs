using IMS.Shared.DTOs.Product.Request;
using IMS.Shared.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDetailResponseDto>> GetAllProducts();
        Task<List<ProductDetailResponseDto>> GetProductsByCategory(Guid Id);
        Task SaveProduct(SaveProductRequestDto request);
        Task<ProductDetailResponseDto> GetProductById(Guid Id);
        Task DeleteProduct(Guid Id);

    }
}
