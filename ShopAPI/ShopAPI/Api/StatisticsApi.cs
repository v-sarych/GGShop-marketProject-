using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Model.Entities.DTO.Statistics;
using ShopApi.Model.Interfaces.Repository;
using ShopDb.Entities;

namespace ShopApi.Api
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/Statistics")]
    [ApiController]
    public class StatisticsApi : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsApi(IStatisticsRepository statisticsRepository) => _statisticsRepository = statisticsRepository;

        [HttpGet("GetMonth")]
        public async Task<BaseStatisticsDTO> GetBaseMonthStatistics()
            => await _statisticsRepository.GetBaseStatistics(DateTime.Now.AddMonths(-1), DateTime.Now);
    }
}
