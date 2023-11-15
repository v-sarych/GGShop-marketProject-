using AutoMapper;
using ShopApiCore.Mapping;
using ShopApiCore.Mapping.Profiles.DTO;
using ShopDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace Tests.Tests.Repository
{
    public abstract class RepositoryTestBase : IDisposable
    {
        protected ShopDbContext DbContext;
        protected IMapper Mapper;
        public RepositoryTestBase()
        {
            DbContext = TestShopDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg => AutoMapperConfiguration.GetConfiguration(cfg));
            Mapper = configurationProvider.CreateMapper(); 
        }

        public void Dispose()
        {
            TestShopDbContextFactory.Destroy(DbContext);
        }
    }
}
