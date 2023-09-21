using API.Dtos.ProductDtos;
using AutoMapper;
using Core.Models.Product;

namespace API.Helpers.Profiles
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToRerturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToRerturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
                return _configuration["ApiUrl"] + source.PictureUrl;
            return null;
        }
    }
}