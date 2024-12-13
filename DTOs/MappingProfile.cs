using AutoMapper;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<VoucherDTO, Voucher>().ReverseMap();
            CreateMap<RegisterDTO, AppUser>();
        }
    }
}
