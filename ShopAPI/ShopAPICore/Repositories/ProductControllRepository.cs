using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopApiCore.Entities.DTO.ProductControll;
using ShopApiCore.Interfaces.Repository;
using ShopDb;
using ShopDb.Entities;

namespace ShopApiCore.Repositories
{
    public class ProductControllRepository : IProductControllRepository
    {
        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;

        public ProductControllRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);

        public async Task<int> Create()// Return id of created product
        {
            Product creatingProduct = new();
            await _dBContext.Products.AddAsync(creatingProduct);
            await _dBContext.SaveChangesAsync();

            return creatingProduct.Id;
        }

        public async Task Update(UpdateProductDTO productDTO)
        {

            Product dBProduct = await _dBContext.Products
                .Include(product => product.Tags)
                .FirstAsync(p => p.Id == productDTO.Id);

            dBProduct.Name = productDTO.Name ?? dBProduct.Name;
            dBProduct.Description = productDTO.Description ?? dBProduct.Description;
            dBProduct.CanBeFound = productDTO.CanBeFound ?? dBProduct.CanBeFound;

            if (productDTO.TagsIds != null)
            {
                dBProduct.Tags.Clear();
                foreach (var tagId in productDTO.TagsIds)
                    dBProduct.Tags.Add(
                        await _dBContext.Tags.FirstAsync(x => x.Id == tagId));
            }

            await _dBContext.SaveChangesAsync();
        }
    }
}
