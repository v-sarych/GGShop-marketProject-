using AutoMapper;
using ShopApiCore.Entities.DTO.Package;
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

        public async Task Create(CreateProductAvailabilityDTO settings)
        {
            AvailabilityOfProduct availabilityOfProduct = _mapper.Map<AvailabilityOfProduct>(settings);
            await _dBContext.AvailabilityOfProducts.AddAsync(availabilityOfProduct);

            await _dBContext.SaveChangesAsync();
        }

        public async Task Update(UpdateProductAvailabilityDTO settings)
        { 
            AvailabilityOfProduct availability = _dBContext.AvailabilityOfProducts.FirstOrDefault(x => x.Sku == settings.Sku);

            availability.Cost = settings.Cost ?? availability.Cost;
            availability.Count = settings.Count ?? availability.Count;
            availability.Weight = settings.Weight ?? availability.Weight;
            availability.PackegeSizeId = settings.PackageId ?? availability.PackegeSizeId;

            await _dBContext.SaveChangesAsync();
        }


        public Task CreatepackageSize(PackageSizeDTO packageSize)
        {
            throw new NotImplementedException();
        }

        public Task DeletePackageSize(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<FullPackageSizeInfoDTO>> GetPackageSizes()
        {
            throw new NotImplementedException();
        }
    }
}
