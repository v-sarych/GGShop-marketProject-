using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopAPICore.Entities.DTO.Package;
using ShopAPICore.Entities.DTO.ProductAvailability;
using ShopAPICore.Exceptions;
using ShopAPICore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopAPICore.Repositories;

public class ProductAvailabilityRepository : IProductAvailabilityRepository
{
    private readonly IShopDbContext _dBContext;
    private readonly IMapper _mapper;

    public ProductAvailabilityRepository(IShopDbContext dBContext, IMapper mapper)
        => (_dBContext, _mapper) = (dBContext, mapper);

    public async Task Create(CreateProductAvailabilityDTO settings)
    {
        if (await _dBContext.AvailabilityOfProducts.AsNoTracking().AnyAsync(x => x.Sku == settings.Sku))
            throw new AlreadyExistException();

        var availabilityOfProduct = _mapper.Map<AvailabilityOfProduct>(settings);
        await _dBContext.AvailabilityOfProducts.AddAsync(availabilityOfProduct);

        await _dBContext.SaveChangesAsync();
    }

    public async Task Update(UpdateProductAvailabilityDTO settings)
    { 
        var availability = _dBContext.AvailabilityOfProducts.First(x => x.Sku == settings.Sku);

        availability.Cost = settings.Cost ?? availability.Cost;
        availability.Count = settings.Count ?? availability.Count;
        availability.Weight = settings.Weight ?? availability.Weight;
        availability.PackageSizeId = settings.PackageId ?? availability.PackageSizeId;

        await _dBContext.SaveChangesAsync();
    }


    public async Task<int> CreatepackageSize(PackageSizeDTO packageSize)
    {
        if(await _dBContext.PackageSizes.AsNoTracking().AnyAsync(x => x.Height == packageSize.Height && x.Width == packageSize.Width && x.Length == packageSize.Length))
            throw new AlreadyExistException();

        var package = _mapper.Map<PackageSize>(packageSize);
        await _dBContext.PackageSizes.AddAsync(package);

        await _dBContext.SaveChangesAsync();

        return (await _dBContext.PackageSizes.AsNoTracking()
            .FirstAsync(x => x.Height == packageSize.Height && x.Width == packageSize.Width && x.Length == packageSize.Length)).Id;
    }

    public async Task DeletePackageSize(int id)
    {
        _dBContext.PackageSizes.Remove(_dBContext.PackageSizes.AsTracking().First(x => x.Id == id));
        await _dBContext.SaveChangesAsync();
    }

    public async Task<ICollection<FullPackageSizeInfoDTO>> GetPackageSizes()
        => await _dBContext.PackageSizes.ProjectTo<FullPackageSizeInfoDTO>(_mapper.ConfigurationProvider).ToListAsync();
}