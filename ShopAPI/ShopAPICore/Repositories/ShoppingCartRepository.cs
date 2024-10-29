using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopAPICore.Entities.DTO.ShoppingCart;
using ShopAPICore.Exceptions;
using ShopAPICore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopAPICore.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IShopDbContext _dBContext;
    private readonly IMapper _mapper;

    public ShoppingCartRepository(IShopDbContext dBContext, IMapper mapper)
        => (_dBContext, _mapper) = (dBContext, mapper);

    public async Task<ICollection<GetShoppingCartElementDTO>> Get(long userId)
    {
        List<GetShoppingCartElementDTO> elements = await _dBContext.UsersShoppingCartItems.AsNoTracking()
            .Include(item => item.AvailabilityOfProduct)
            .Include(item => item.AvailabilityOfProduct.Product)
            .Where(item => item.UserId == userId)
            .ProjectTo<GetShoppingCartElementDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        List<AvailabilityOfProduct> availabilitiesOfProduct = await _dBContext.AvailabilityOfProducts.AsNoTracking()
            .Where(a => elements.Select(e => e.Sku).Any(e => e == a.Sku))
            .ToListAsync();

        foreach (var element in elements)
        {
            var availabilityOfProduct = availabilitiesOfProduct.FirstOrDefault(a => a.Sku == element.Sku);

            if (availabilityOfProduct.Count >= element.Count)
                element.Cost = (float)availabilityOfProduct.Cost * element.Count;// без внешнего ключа
            else
                element.AvailablуInStock = availabilityOfProduct.Count;
        }

        return elements;
    }

    public async Task AddItem(AddShoppingCartItemDTO item, long userId)
    {
        if (await _dBContext.UsersShoppingCartItems.AsNoTracking().AnyAsync(x => x.UserId == userId && x.Sku == item.Sku))
            throw new AlreadyExistException();

        var dBItem = _mapper.Map<UserShoppingCartItem>(item);
        dBItem.UserId = userId;

        await _dBContext.UsersShoppingCartItems.AddAsync(dBItem);
        await _dBContext.SaveChangesAsync();
    }

    public async Task UpdateItemCount(UpdateShoppingCartItemDTO itemWithChanges, long userId)
    {
        var dBItem = await _dBContext.UsersShoppingCartItems.FirstAsync(item => item.UserId == userId 
            && item.Sku == itemWithChanges.Sku);

        dBItem.Count = itemWithChanges.Count;
        await _dBContext.SaveChangesAsync();
    }

    public async Task RemoveItem(string sku, long userId)
    {
        _dBContext.UsersShoppingCartItems.Remove(await _dBContext.UsersShoppingCartItems.FirstAsync(x => x.UserId == userId
            && x.Sku == sku));

        await _dBContext.SaveChangesAsync();
    }
}