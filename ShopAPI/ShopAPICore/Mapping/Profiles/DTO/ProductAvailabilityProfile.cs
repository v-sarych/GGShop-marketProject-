using AutoMapper;
using ShopAPICore.Entities.DTO.Package;
using ShopAPICore.Entities.DTO.ProductAvailability;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

public class ProductAvailabilityProfile : Profile
{
    public ProductAvailabilityProfile()
    {
        CreateMap<AvailabilityOfProduct, GetProductAvailabilityDTO>();
        CreateMap<AvailabilityOfProduct, AvailabilityForGetOrderItemDTO>();
        CreateMap<UpdateProductAvailabilityDTO, AvailabilityOfProduct>();
        CreateMap<CreateProductAvailabilityDTO, AvailabilityOfProduct>();
        CreateMap<AvailabilityOfProduct, SimpleAvailabilityDTO>();
        CreateMap<AvailabilityOfProduct, AvailabilityForGetShoppingCartDTO>();
        //.ForMember(x => x.Product, y => y.Ignore());

        CreateMap<PackageSize, PackageSizeDTO>();
        CreateMap<PackageSizeDTO, PackageSize>();
        CreateProjection<PackageSize, FullPackageSizeInfoDTO>();
    }
}