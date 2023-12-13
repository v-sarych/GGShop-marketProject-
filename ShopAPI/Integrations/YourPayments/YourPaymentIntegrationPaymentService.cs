using Integrations.YourPayments.Entities.AuthorizePayment;
using Integrations.YourPayments.Interfaces;
using Microsoft.Extensions.Logging;
using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Exceptions;
using ShopApiCore.Interfaces.Services;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace Integrations.YourPayments
{
    public class YourPaymentIntegrationPaymentService : IPaymentService
    {
        private readonly PaymentServiceUnitOfWork _unitOfWork;
        public YourPaymentIntegrationPaymentService(PaymentServiceUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<string> CheckPaymentStatus(Guid OrderId)
            => await _unitOfWork.PaymentRepository.GetPaymentStatus(OrderId);

        public async Task<string> CreateAndAuthorizePayment(PaymentInfoDTO info)
        {
            if (!(await _unitOfWork.PaymentRepository.CanCreatePayment(info.OrderId)))
                throw new AlreadyExistException();

            HttpClient client = _unitOfWork.HttpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(await _createAuthorizeMessage(info));
            string responseText = await response.Content.ReadAsStringAsync();

            _unitOfWork.Logger.LogInformation(responseText);

            if (response.IsSuccessStatusCode)
                await _unitOfWork.PaymentRepository.CreatePayment(new()
                {
                    OrderId = info.OrderId,
                    AdditionalDetails = responseText,
                    IdInPaymentGateway = Guid.NewGuid()
                });

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpRequestMessage> _createAuthorizeMessage(PaymentInfoDTO info)
        {
            AuthorizePaymentData authorizeData = await _unitOfWork.PaymentDataCreator.GetAuthorizePaymentData(info);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, _unitOfWork.Configuration.AuthorizePaymentUrl);

            string contentString = JsonSerializer.Serialize(authorizeData);
            message.Content = new StringContent(contentString);

            string merchant = _unitOfWork.Configuration.MerchantId;
            string date = DateTime.Now.ToString();

            message.Headers.Add("X-Header-Merchant", merchant);
            message.Headers.Add("X-Header-Date", date);

            string mD5BodySum = await _unitOfWork.PaymentDataCreator.GetMD5(contentString);
            message.Headers.Add("X-Header-Signature", await _unitOfWork.PaymentDataCreator.GetSignature(new Entities.SignatureParameters()
            {
                BaseUrl = _unitOfWork.Configuration.AuthorizePaymentUrl,
                Date = date,
                HttpMethod = "POST",
                Merchant = merchant,
                MD5BodySum = mD5BodySum
            }));
            return message;
        }
    }
}
