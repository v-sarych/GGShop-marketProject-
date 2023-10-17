using AutoMapper;
using ShopApiCore.Entities.DTO.ProductAvailability;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class ProductAvailabilityProfile : Profile
    {
        public ProductAvailabilityProfile()
        {
            CreateMap<AvailabilityOfProduct, GetProductAvailabilityDTO>();
            CreateMap<UpdateProductAvailabilityDTO, AvailabilityOfProduct>();
            CreateMap<CreateProductAvailabilityDTO, AvailabilityOfProduct>();
            CreateMap<AvailabilityOfProduct, SimpleAvailabilityDTO>();
            CreateMap<AvailabilityOfProduct, AvailabilityForGetShoppingCartDTO>();
        }
    }
}
