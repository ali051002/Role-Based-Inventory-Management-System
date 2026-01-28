using AutoMapper;
using IMS.Application.Interfaces.Repositories;
using IMS.Application.Interfaces.Services;
using IMS.Shared.DTOs.Product.Request;
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
        public async Task DeleteProduct(Guid Id)
        {
            try
            {
                await _iProductRepository.DeleteProduct(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<ProductDetailResponseDto>> GetAllProducts()
        {
            try
            {
                return await _iProductRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ProductDetailResponseDto> GetProductById(Guid Id)
        {
            try
            {
                return await _iProductRepository.GetProductById(Id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProductDetailResponseDto>> GetProductsByCategory(Guid Id)
        {
            try
            {
                return await _iProductRepository.GetProductsByCategory(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveProduct(SaveProductRequestDto request)
        {
            try
            {
                await _iProductRepository.SaveProduct(request);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
