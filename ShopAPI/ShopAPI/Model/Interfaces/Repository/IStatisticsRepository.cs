using ShopApi.Model.Entities.DTO.Statistics;

namespace ShopApi.Model.Interfaces.Repository
{
    public interface IStatisticsRepository
    {
        Task<BaseStatisticsDTO> GetBaseStatistics(DateTime beginingPeriod, DateTime endPeriod);
    }
}
