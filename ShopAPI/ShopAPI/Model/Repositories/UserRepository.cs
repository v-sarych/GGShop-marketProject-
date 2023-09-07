﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopApi.Model.Entities.DTO.User;
using ShopApi.Model.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopApi.Model.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;
        public UserRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);

        public async Task<GetUserDTO> Get(long id)
        {
            GetUserDTO user = await _dBContext.Users.AsNoTracking()
               .Where(user => user.Id == id)
               .ProjectTo<GetUserDTO>(_mapper.ConfigurationProvider)
               .FirstAsync();

            foreach (var shoppingCartItem in user.UserShoppingCartItems) 
            {
                AvailabilityOfProduct availabilityOfProduct = await _dBContext.AvailabilityOfProducts.AsNoTracking()
                                            .FirstOrDefaultAsync(a => a.Size == shoppingCartItem.Size
                                                && a.ProductId == shoppingCartItem.Product.Id
                                                && a.Count > 0);
                if (availabilityOfProduct != null)
                    shoppingCartItem.Cost = (float)availabilityOfProduct.Cost * shoppingCartItem.Count;
            }

            return user;
        }


        public async Task UpdateInfo(UpdateUserInfoDTO updateInfo, long id)
        {
            User user  = await _dBContext.Users.FirstAsync(u => u.Id == id);

            user.Name = updateInfo.Name ?? user.Name;

            await _dBContext.SaveChangesAsync();
        }
    }
}
