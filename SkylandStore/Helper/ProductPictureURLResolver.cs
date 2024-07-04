using AutoMapper;
using AutoMapper.Execution;
using SkelandStore.Core.Entities;
using SkylandStore.DTOs;

namespace SkylandStore.Helper
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductToReturnDTo, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDTo destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["APIBaseURL"]}{source.PictureUrl}";
            }
            return " ";
        }
    }
}
