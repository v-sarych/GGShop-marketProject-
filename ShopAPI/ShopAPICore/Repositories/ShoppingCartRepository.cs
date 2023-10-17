﻿using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopApiCore.Entities.DTO.ShoppingCart;
using ShopApiCore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopApiCore.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);

        public async Task<ICollection<GetShoppingCartElementDTO>> Get(long userId)
        {
            List<GetShoppingCartElementDTO> elements = await _dBContext.UsersShoppingCartItems.AsNoTracking()
                       .Include(item => item.Product)
                       .Include(item => item.AvailabilityOfProduct)
                       .Where(item => item.UserId == userId)
                       .ProjectTo<GetShoppingCartElementDTO>(_mapper.ConfigurationProvider)
                       .ToListAsync();

            foreach(var element in elements)
            {
                AvailabilityOfProduct availabilityOfProduct = await _dBContext.AvailabilityOfProducts.AsNoTracking()
                            .FirstOrDefaultAsync(a => a.Sku == element.Sku
                                && a.Count > 0);

                if (availabilityOfProduct != null)
                    element.Cost = (float)availabilityOfProduct.Cost * element.Count;// без внешнего ключа
            }

            return elements;
        }

        public async Task<long> AddItem(AddShoppingCartItemDTO item, long userId)
        {
            UserShoppingCartItem dBItem = _mapper.Map<UserShoppingCartItem>(item);
            dBItem.UserId = userId;

            await _dBContext.UsersShoppingCartItems.AddAsync(dBItem);
            await _dBContext.SaveChangesAsync();

            return dBItem.Id;
        }

        public async Task UpdateItemCount(UpdateShoppingCartItemDTO itemWithChanges, long userId)
        {
            UserShoppingCartItem dBItem = await _dBContext.UsersShoppingCartItems.FirstAsync(item => item.UserId == userId 
                                                        && item.Id == itemWithChanges.Id);

            dBItem.Count = itemWithChanges.Count;
            await _dBContext.SaveChangesAsync();
        }

        public async Task RemoveItem(long itemId, long userId)
        {
            _dBContext.UsersShoppingCartItems.Remove(await _dBContext.UsersShoppingCartItems.FirstAsync(x => x.UserId == userId
                                                                    && x.Id == itemId));

            await _dBContext.SaveChangesAsync();
        }
    }
}
