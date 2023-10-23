using Integrations.Cdek.Interfaces;
using ShopApiCore.Interfaces.Services;
using ShopDb.Entities;
using System.Net.Http;

namespace Integrations.Cdek
{
    public class CdekIntegrationDeliveryService : IDeliveryService
    {
        private readonly IOAuthTokenFactory _tokenFactory;
        public CdekIntegrationDeliveryService(IOAuthTokenFactory oAuthTokenFactory)
            => _tokenFactory = oAuthTokenFactory;
        public Task TransferToDelivery(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
