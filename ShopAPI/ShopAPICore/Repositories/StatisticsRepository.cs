using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopAPICore.Entities.DTO.ProductSearch;
using ShopAPICore.Entities.DTO.Statistics;
using ShopAPICore.Interfaces.Repository;
using ShopDb;

namespace ShopAPICore.Repositories;

public class StatisticsRepository : IStatisticsRepository
{

    private readonly IShopDbContext _dBContext;
    private readonly IMapper _mapper;

    public StatisticsRepository(IShopDbContext dBContext, IMapper mapper)
        => (_dBContext, _mapper) = (dBContext, mapper);
    public async Task<BaseStatisticsDTO> GetBaseStatistics(DateTime beginingPeriod, DateTime endPeriod)
    {
        var statistics = new BaseStatisticsDTO();

        statistics.OrderCount = await _dBContext.Orders.AsNoTracking()
            .Where(o => o.DateOfCreation > beginingPeriod && o.DateOfCreation < endPeriod)
            .CountAsync();

        statistics.SumOfOrders = _dBContext.Orders.AsNoTracking()
            .Where(o => o.DateOfCreation > beginingPeriod && o.DateOfCreation < endPeriod)
            .Select(o => o.Cost)
            .Sum();
        statistics.AverageCheck = statistics.SumOfOrders / _dBContext.Orders.AsNoTracking()
            .Where(o => o.DateOfCreation > beginingPeriod && o.DateOfCreation < endPeriod)
            .Count();

        statistics.MostPopularProducts = _dBContext.Products.AsNoTracking()
            .OrderByDescending(x => x.OrderItems
                .Count(i => i.Order.DateOfCreation > beginingPeriod && i.Order.DateOfCreation  < endPeriod))
            .Take(3)
            .ProjectTo<SimpleProductDTO>(_mapper.ConfigurationProvider)
            .ToArray();
            
        return statistics;
    }
}