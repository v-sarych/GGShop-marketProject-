using AutoMapper;
using ShopApiCore.Entities.DTO.ProductControll;
using ShopApiCore.Entities.DTO.ProductSearch;
using ShopApiCore.Entities.DTO.SearchResults;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateProjection<Product, AllProductInfoDTO>();
            CreateProjection<Product, ExtendedProductInfoDTO>();

            CreateProjection<Product, ProductSearchResultDTO>();

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(prop => prop.Tags, opt => opt.Ignore());
            CreateMap<Product, SimpleProductDTO>();
        }
    }
}
