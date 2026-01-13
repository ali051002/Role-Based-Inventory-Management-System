using AutoMapper;
using IMS.Domain.Entities;
using IMS.Shared.DTOs.Category.Response;
using IMS.Shared.DTOs.Product.Response;
using IMS.Shared.DTOs.StockTransaction.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Product, ProductDetailResponseDto>().ReverseMap();
             CreateMap<Category, CategoryDetailResponseDto>().ReverseMap();
             CreateMap<StockTransaction, StockTransactionDetailResponseDto>().ReverseMap();
        }
    }
}
