using AutoMapper;
using OnlineSouvenirShopAPI.DTOs.CategoryDTOs;
using OnlineSouvenirShopAPI.DTOs.ProductDTOs;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();

            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();

            CreateMap<VoucherDTO, Voucher>().ReverseMap();

            CreateMap<RegisterDTO, AppUser>();
        }
    }
}
