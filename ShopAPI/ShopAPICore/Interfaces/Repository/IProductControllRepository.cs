﻿using ShopApiCore.Entities.DTO.ProductControll;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IProductControllRepository
    {
        Task<int> Create();
        Task Update(UpdateProductDTO productDTO);
    }
}
