using ShopApiCore.Entities.DTO.ShoppingCart;
using ShopApiCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Tests.Repository.UserRepository
{
    public class GetTests : RepositoryTestBase
    {
        [Fact]
        public async Task Get2id_ProductsNotNullInItems()
        {
            var repository = new ShopApiCore.Repositories.UserRepository(DbContext, Mapper);
            long id = 2;

            var user = await repository.Get(2);

            Assert.True(user.UserShoppingCartItems.All(x => x.AvailabilityOfProduct.Product != null));
        }

        [Fact]
        public async Task Get2id_AvailabilityNotNullInItems()
        {
            var repository = new ShopApiCore.Repositories.UserRepository(DbContext, Mapper);
            long id = 2;

            var user = await repository.Get(2);

            Assert.True(user.UserShoppingCartItems.All(x => x.AvailabilityOfProduct != null));
        }
    }
}
