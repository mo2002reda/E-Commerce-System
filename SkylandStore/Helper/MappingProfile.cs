using AutoMapper;
using SkelandStore.Core.Entities;
using SkelandStore.Core.Entities.Identity;
using SkelandStore.Core.Entities.Order_Aggregation;
using SkylandStore.DTOs;
using SkylandStore.Helper;
using IdentityAddress = SkelandStore.Core.Entities.Identity.Address;
using AggregateAddress = SkelandStore.Core.Entities.Order_Aggregation.Address;

namespace SkylandStore.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTo>()
                    .ForMember(d => d.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                    .ForMember(d => d.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                    .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureURLResolver>());
            CreateMap<AddressDTO, IdentityAddress>().ReverseMap();
            CreateMap<AggregateAddress, AddressDTO>();
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
