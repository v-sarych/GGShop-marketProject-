using Integrations.YourPayments.Entities.AuthorizePayment;
using Integrations.YourPayments.Entities.AuthorizePayment.Response;
using Integrations.YourPayments.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using ShopApiCore.Entities.DTO.Payments;
using ShopApiCore.Exceptions;
using ShopApiCore.Interfaces.Services;
using System.Globalization;
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

            HttpRequestMessage request = await _createAuthorizeMessage(info);

            ////////logging            
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString()); if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();
            HttpResponseMessage response = await client.SendAsync(request);
            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString()); if (response.Content != null)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();
            ///////

            //HttpResponseMessage response = await client.SendAsync(await _createAuthorizeMessage(info));
            string responseText = await response.Content.ReadAsStringAsync();

            _unitOfWork.Logger.LogInformation(responseText);

            if (response.IsSuccessStatusCode)
                await _unitOfWork.PaymentRepository.CreatePayment(new()
                {
                    OrderId = info.OrderId,
                    AdditionalDetails = responseText,
                    IdInPaymentGateway = JsonSerializer.Deserialize<AuthorizePaymentResponse>(responseText).PayuPaymentReference
                });

            return responseText;
        }

        private async Task<HttpRequestMessage> _createAuthorizeMessage(PaymentInfoDTO info)
        {
            AuthorizePaymentData authorizeData = await _unitOfWork.PaymentDataCreator.GetAuthorizePaymentData(info);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, _unitOfWork.Configuration.GatewayHost + _unitOfWork.Configuration.AuthorizePaymentUrl);

            string contentString = JsonSerializer.Serialize(authorizeData, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });
            message.Content = new StringContent(contentString, System.Text.Encoding.UTF8, "application/json");

            string merchant = _unitOfWork.Configuration.MerchantId;
            string date = DateTime.Now.ToString("yyyy-MM-dd\\THH:mm:sszzz");
            _unitOfWork.Logger.LogInformation(date);

            message.Headers.Add("X-Header-Merchant", merchant);
            message.Headers.Add("X-Header-Date", date);

            string mD5BodySum = await _unitOfWork.PaymentDataCreator.GetMD5(await message.Content.ReadAsByteArrayAsync());
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
