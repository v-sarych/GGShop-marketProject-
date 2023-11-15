using ShopApiCore.Entities.DTO.ProductSearch;
using ShopApiCore.Repositories;
using System.Threading.Tasks;
using Tests.Common;

namespace Tests.Tests.Repository.ProductSearchRepository
{
    public class SearchTests : RepositoryTestBase
    {
        [Fact]
        public async Task Search_CanBeFoundTest_testByTagId1()
        {
            var repository = new ShopApiCore.Repositories.ProductSearchRepository(DbContext, Mapper);

            int? count = (await repository.Search(new ProductSearchSettingsDTO() { 
                TagIds = new[] { 1 }
            }))?.Count;

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task Search_SearchBy1TagTest()
        {
            var repository = new ShopApiCore.Repositories.ProductSearchRepository(DbContext, Mapper);

            int? count = (await repository.Search(new ProductSearchSettingsDTO()
            {
                TagIds = new[] { 1 }
            }))?.Count;

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task Search_SearchBy2TagsTest()
        {
            var repository = new ShopApiCore.Repositories.ProductSearchRepository(DbContext, Mapper);

            int? count = (await repository.Search(new ProductSearchSettingsDTO()
            {
                TagIds = new[] { 1, 2 }
            }))?.Count;

            Assert.Equal(1, count);
        }
    }
}
