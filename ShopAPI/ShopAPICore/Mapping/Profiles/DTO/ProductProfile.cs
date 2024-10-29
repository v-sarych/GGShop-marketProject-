using AutoMapper;
using ShopAPICore.Entities.DTO.ProductControll;
using ShopAPICore.Entities.DTO.ProductSearch;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

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