using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApi.Model.Entities.DTO.Statistics;
using ShopApi.Model.Interfaces.Repository;
using ShopDb;

namespace ShopApi.Model.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {

        private readonly IShopDbContext _dBContext;
        private readonly IMapper _mapper;

        public StatisticsRepository(IShopDbContext dBContext, IMapper mapper)
            => (_dBContext, _mapper) = (dBContext, mapper);
        public async Task<BaseStatisticsDTO> GetBaseStatistics(DateTime beginingPeriod, DateTime endPeriod)
        {
            BaseStatisticsDTO statistics = new BaseStatisticsDTO();

            statistics.OrderCount = await _dBContext.Orders.AsNoTracking()
                .Where(o => o.DateOfCreation > beginingPeriod && o.DateOfCreation < endPeriod)
                .CountAsync();

            float[] checks = _dBContext.Orders.AsNoTracking()
                .Where(o => o.DateOfCreation > beginingPeriod && o.DateOfCreation < endPeriod)
                .Select(o => o.Cost)
                .ToArray();
            foreach (var check in checks)
                statistics.SumOfOrders += check;
            statistics.AverageCheck = statistics.SumOfOrders / checks.Length;

            statistics.MostPopularProducts = _dBContext.OrdersItems.AsNoTracking()
                .Where(o => o.Ored.DateOfCreation > beginingPeriod && o.DateOfCreation < endPeriod)
                .ToArray();


            return statistics;
        }
    }
}
