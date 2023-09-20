using API.Dtos.ProductDtos;
using AutoMapper;
using Core.Models.Product;

namespace API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductToRerturnDto>()
                .ForMember(p => p.ProductType, opt => opt.MapFrom(src => src.ProdcutType.Name))
                .ForMember(p => p.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name));
        }
    }
}