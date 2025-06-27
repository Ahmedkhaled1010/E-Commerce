using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObject.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplemetation.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(dist=>dist.productBrand,options=>options.MapFrom(src=>src.ProductBrand.Name))
               .ForMember(dist => dist.productType, options => options.MapFrom(src => src.ProductType.Name))
               .ForMember(dist => dist.PictureUrl, options=>options.MapFrom<PictureUrlReselover>());
            CreateMap<ProductType, TypeDto>();
            CreateMap<ProductBrand,BrandDto>();
        }
    }
}
