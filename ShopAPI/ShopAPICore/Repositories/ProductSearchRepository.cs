using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopAPICore.Entities.DTO.ProductSearch;
using ShopAPICore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopAPICore.Repositories;

public class ProductSearchRepository : IProductSearchRepository
{
    private readonly IShopDbContext _dBContext;
    private readonly IMapper _mapper;
    public ProductSearchRepository(IShopDbContext dBContext, IMapper mapper)
        => (_dBContext, _mapper) = (dBContext, mapper);

    public async Task<ICollection<ProductSearchResultDTO>> Search(ProductSearchSettingsDTO settings)
    {
        IQueryable<Product> resultQuery = _dBContext.Products.AsNoTracking()
            .Include(x => x.AvailabilitisOfProduct)
            .Where(product => product.CanBeFound);

        if(settings.Name != null)
            resultQuery = resultQuery.Where(p => EF.Functions.Like(p.Name, $"%{ settings.Name }%"));

        if (settings.TagIds != null)
            foreach (var tagId in settings.TagIds)
                resultQuery = resultQuery.Where(product => product.Tags.Any(t => t.Id == tagId));

        /*if (settings.TagsIds != null)
            resultQuery = resultQuery.Where(product => product.Tags
                            .Select(t => t.Id)
                            .Intersect(settings.TagsIds).Count() == settings.TagsIds.Count());*/// второй вариант поиска по тегам но с сложностью О(n^2)
        if(settings.InStock)
            resultQuery = resultQuery.Where(product => product.AvailabilitisOfProduct.Any(a => a.Count > 0));

        if (settings.FirstRangePoint != null && settings.EndRangePoint != null)
            resultQuery = resultQuery.Skip((int)settings.FirstRangePoint)
                .Take((int)settings.EndRangePoint - (int)settings.FirstRangePoint);
            
        return await resultQuery.ProjectTo<ProductSearchResultDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ICollection<SimpleProductDTO>> SearchWithoutAccounting(ProductSearchSettingsDTO settings, bool useCanBeFound)
    {
        IQueryable<Product> resultQuery = _dBContext.Products.AsNoTracking();

        if (useCanBeFound)
            resultQuery = resultQuery.Where(product => product.CanBeFound);

        if (settings.Name != null)
            resultQuery = resultQuery.Where(p => EF.Functions.Like(p.Name, $"%{settings.Name}%"));

        if (settings.TagIds != null)
            foreach (var tagId in settings.TagIds)
                resultQuery = resultQuery.Where(product => product.Tags.Any(t => t.Id == tagId));

        if (settings.FirstRangePoint != null && settings.EndRangePoint != null)
            resultQuery = resultQuery.Skip((int)settings.FirstRangePoint)
                .Take((int)settings.EndRangePoint - (int)settings.FirstRangePoint);

        return await resultQuery.ProjectTo<SimpleProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<AllProductInfoDTO> GetAllProductInfo(int id)
        => await _dBContext.Products.AsNoTracking()
            .Include(product => product.Tags)
            .Include(product => product.AvailabilitisOfProduct)
            .Where(product => product.Id == id)
            .ProjectTo<AllProductInfoDTO>(_mapper.ConfigurationProvider)
            .FirstAsync();

    public async Task<ExtendedProductInfoDTO> GetExtendedProductInfo(int id) 
        => await _dBContext.Products.AsNoTracking()
            .Include(product => product.Tags)
            .Include(product => product.AvailabilitisOfProduct)
            .Where(product => product.Id == id)
            .ProjectTo<ExtendedProductInfoDTO>(_mapper.ConfigurationProvider)
            .FirstAsync();
}