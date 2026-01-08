using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Services
{
    public class ProductService(IProductRepository _iProductRepository, IMapper _mapper) : IProductService
    {
        public async Task<List<ProductDetailResponseDto>> GetAllProducts()
        {
            try
            {
                return await _iProductRepository.GetAllProducts();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
