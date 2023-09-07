using AutoMapper;
using ShopApi.Model.Entities.DTO.ProductControll;
using ShopApi.Model.Entities.DTO.ProductSearch;
using ShopApi.Model.Entities.DTO.SearchResults;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateProjection<Product, AllProductInfoDTO>();
            CreateProjection<Product, ExtendedProductInfoDTO>();
            CreateProjection<Product, SimpleProductDTO>();

            CreateProjection<Product, ProductSearchResultDTO>();

            CreateMap<UpdateProductDTO, Product>()
                .ForMember(prop => prop.Tags, opt => opt.Ignore());
        }
    }
}
