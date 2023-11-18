using Integrations.YourPayments;
using ShopDb;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace Tests.Tests.IntegrationsComponentsTest.YourPayments.PaymentDataCreator
{
    public class GetAuthorizePaymentDataTests : IDisposable
    {
        private readonly ShopDbContext _dbContext;
        public GetAuthorizePaymentDataTests()
        {
            _dbContext = TestShopDbContextFactory.Create();
        }

        [Fact]
        public async Task Order1_NotNull_YouCanSeeAllDataThroughDebuger()
        {
            var dataCerator = new Integrations.YourPayments.PaymentDataCreator(new(), _dbContext);
            var parametr = new ShopApiCore.Entities.DTO.Payments.PaymentInfoDTO()
            {
                Currency = "RUB",
                PaymentMethod = "card",
                OrderId = Guid.Parse("ecabc8b5-93e9-4732-84b8-e2401dd2119c"),
                paymentClientDTO = new()
                {
                    CountryCode = "RU",
                    Email = "d@d.d",
                    FirstName = "s",
                    LastName = "s"
                }
            };

            var data = await dataCerator.GetAuthorizePaymentData(parametr);

            Assert.NotNull(data.Products);
        }

        public void Dispose() 
        {
            TestShopDbContextFactory.Destroy(_dbContext);
        }
    }
}
