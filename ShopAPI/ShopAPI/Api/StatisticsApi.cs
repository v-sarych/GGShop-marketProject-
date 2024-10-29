using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPICore.Entities.DTO.Statistics;
using ShopAPICore.Interfaces.Repository;
using ShopDb.Enums;

namespace ShopApiServer.Api;

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