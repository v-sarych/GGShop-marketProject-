using Integrations.Cdek.Interfaces;
using ShopApiCore.Interfaces.Services;
using ShopDb;
using ShopDb.Entities;
using System.Net.Http;

namespace Integrations.Cdek
{
    public class CdekIntegrationDeliveryService : IDeliveryService
    {
        public ShopDbContext _dbContext { get; set; }
        private readonly IOAuthTokenFactory _tokenFactory;
        public CdekIntegrationDeliveryService(IOAuthTokenFactory oAuthTokenFactory, ShopDbContext dbContext)
            => (_tokenFactory, _dbContext) = (oAuthTokenFactory, dbContext);
        public Task TransferToDelivery(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
