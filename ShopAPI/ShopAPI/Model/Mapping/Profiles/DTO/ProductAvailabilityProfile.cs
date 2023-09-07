using AutoMapper;
using ShopApi.Model.Entities.DTO.ProductAvailability;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
{
    public class ProductAvailabilityProfile : Profile
    {
        public ProductAvailabilityProfile()
        {
            CreateMap<AvailabilityOfProduct, GetProductAvailabilityDTO>();
            CreateMap<UpdateProductAvailabilityDTO, AvailabilityOfProduct>();
            CreateMap<CreateProductAvailabilityDTO, AvailabilityOfProduct>();
            CreateMap<AvailabilityOfProduct, SimpleAvailabilityDTO>();
        }
    }
}
