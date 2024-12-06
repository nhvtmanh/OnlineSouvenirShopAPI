using AutoMapper;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Voucher, VoucherDTO>().ReverseMap();
        }
    }
}
