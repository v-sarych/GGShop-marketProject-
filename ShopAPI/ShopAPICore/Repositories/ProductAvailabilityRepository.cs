using AutoMapper;
using ShopApiCore.Entities.DTO.ProductAvailability;
using ShopApiCore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopApiCore.Repositories
{
    public class ProductAvailabilityRepository : IProductAvailabilityRepository
    {
        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;

        public ProductAvailabilityRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);

        public async Task<int> Create(CreateProductAvailabilityDTO settings)
        {
            AvailabilityOfProduct availabilityOfProduct = _mapper.Map<AvailabilityOfProduct>(settings);
            await _dBContext.AvailabilityOfProducts.AddAsync(availabilityOfProduct);

            await _dBContext.SaveChangesAsync();
            return availabilityOfProduct.Id;
        }

        public async Task Update(UpdateProductAvailabilityDTO settings)
        { 
            AvailabilityOfProduct availability = _dBContext.AvailabilityOfProducts.FirstOrDefault(x => x.Id == settings.Id);

            availability.Cost = settings.Cost ?? availability.Cost;
            availability.Count = settings.Count ?? availability.Count;

            await _dBContext.SaveChangesAsync();
        }
    }
}
