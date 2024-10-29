using ShopAPICore.Entities.DTO.Statistics;

namespace ShopAPICore.Interfaces.Repository;

public interface IStatisticsRepository
{
    Task<BaseStatisticsDTO> GetBaseStatistics(DateTime beginingPeriod, DateTime endPeriod);
}