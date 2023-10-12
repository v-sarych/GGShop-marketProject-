using ShopApiCore.Entities.DTO.Statistics;

namespace ShopApiCore.Interfaces.Repository
{
    public interface IStatisticsRepository
    {
        Task<BaseStatisticsDTO> GetBaseStatistics(DateTime beginingPeriod, DateTime endPeriod);
    }
}
