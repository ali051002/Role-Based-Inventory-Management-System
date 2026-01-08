using IMS.Shared.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductDetailResponseDto>> GetAllProducts();
    }
}
