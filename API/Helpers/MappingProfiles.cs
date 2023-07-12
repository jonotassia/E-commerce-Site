using API.Dtos;
using Core.Models;
using AutoMapper;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, GetProductDto>()
                .ForMember(p => p.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
