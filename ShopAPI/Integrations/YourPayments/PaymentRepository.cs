using Integrations.YourPayments.Entities.Payment;
using Integrations.YourPayments.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;
using ShopDb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IShopDbContext _dbContext;
        public PaymentRepository(IShopDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<bool> CanCreatePayment(Guid id)
            => await _dbContext.Payments.AsNoTracking()
                .AnyAsync(p => p.Id == id && p.Status != PaymentStatuses.Success && p.Status != PaymentStatuses.WaitGateway);

        public async Task CreatePayment(PaymentDTO payment)
        {
            Payment dbPayment = new()
            {
                Id = payment.OrderId,
                Status = PaymentStatuses.WaitGateway,
                AdditionalDetails = payment.AdditionalDetails,
                IdInPaymentGateway = payment.IdInPaymentGateway,
                UserId = (await _dbContext.Orders.AsNoTracking().FirstAsync(o => o.Id == payment.OrderId)).UserId
            };
            await _dbContext.Payments.AddAsync(dbPayment);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePaymnentData(UpdatePaymentDTO info)
        {
            Payment payment = await _dbContext.Payments.AsTracking().FirstAsync(x => x.Id == info.Id && x.IdInPaymentGateway == info.IdInGateway);

            payment.Status = info.Status;
            payment.AdditionalDetails = info.AdditionalInfo;

            await _dbContext.SaveChangesAsync();
        }
    }
}
