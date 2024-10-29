using Integrations.YourPayments.Entities.Payment;
using Integrations.YourPayments.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;
using ShopDb.Enums;

namespace Integrations.YourPayments;

public class PaymentRepository : IPaymentRepository
{
    private readonly IShopDbContext _dbContext;
    public PaymentRepository(IShopDbContext dbContext)
    {
        _dbContext= dbContext;
    }

    public async Task<bool> CanCreatePayment(Guid id)
        => !(await _dbContext.Payments.AsNoTracking()
            .AnyAsync(p => p.Id == id && (p.Status == PaymentStatuses.Success || p.Status == PaymentStatuses.WaitGateway)));

    public async Task CreatePayment(PaymentDTO payment)
    {
        Payment dbPayment = new()
        {
            Id = payment.OrderId,
            Status = PaymentStatuses.WaitGateway,
            AdditionalDetails = payment.AdditionalDetails,
            UserId = (await _dbContext.Orders.AsNoTracking().FirstAsync(o => o.Id == payment.OrderId)).UserId
        };
        await _dbContext.Payments.AddAsync(dbPayment);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<string> GetPaymentStatus(Guid id)
        => (await _dbContext.Payments.AsNoTracking().FirstAsync(x => x.Id == id)).Status;

    public async Task UpdatePaymnentData(UpdatePaymentDTO info)
    {
        var payment = await _dbContext.Payments.AsTracking()
            .Include(p => p.Order)
            .FirstAsync(x => x.Id == info.Id);

        payment.Status = info.Status;
        if (info.Status == PaymentStatuses.Success)
            payment.Order.IsPaidFor = true;

        payment.AdditionalDetails = info.AdditionalInfo;

        await _dbContext.SaveChangesAsync();
    }
}