using Microsoft.AspNetCore.Mvc;
using ss_transpo_dss.services.Interfaces;
using ss_transpo_dss.services.Models;
using ss_transpo_dss.services.Models.Responses;

namespace sg_transpo_api.Controller;

[ApiController]
[Route("timing")]
public class TimeTableController(ITimeTableService timeTableService, ILTADataService ltaDataService) : ControllerBase
{
    [HttpGet("getTimeTableByRoute", Name = "GetTimeTable")]
    [ProducesResponseType<TimeTableModel>(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<TimeTableModel> GetTimeTableByRoute([FromQuery(Name="route")]string route)
    {
        return await timeTableService.GetTimingsByRoute(route);
    }
    [HttpGet("getClosestDepartureByRoute", Name = "GetClosestDepartureByRoute")]
    [ProducesResponseType<TimeTableModel>(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<DateTime?> GetClosestDepartureByRoute([FromQuery(Name="route")]string route)
    {
        return await timeTableService.GetClosestDepartureByRoute(route);
    }
    [HttpGet("getRawBusArrivalsByBusStopCode", Name = "GetRawBusArrivalsByBusStopCode")]
    [ProducesResponseType<LTABusArrivalModel>(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<LTABusArrivalModel?> GetRawBusArrivalsByBusStopCode(
        [FromQuery(Name="busStopCode")]string busStopCode,
        [FromQuery(Name="serviceNo")] string? serviceNo)
    {
        return await ltaDataService.GetBusArrivalsByBusCodeAndServiceNo(busStopCode, serviceNo);
    }
    [HttpGet("getBusArrivalsByBusStopCode", Name = "GetBusArrivalsByBusStopCode")]
    [ProducesResponseType<List<LTABusServiceRecord>>(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<List<LTABusServiceRecord>> GetBusArrivalsByBusStopCode(
        [FromQuery(Name="busStopCode")]string busStopCode,
        [FromQuery(Name="serviceNo")] string? serviceNo)
    {
        return await ltaDataService.GetBusArrivalsInMinutesByBusCodeAndServiceNo(busStopCode, serviceNo);
    }
    [HttpGet("getKwbDecision", Name = "GetKwbDecision")]
    [ProducesResponseType<DecisionResponse>(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<DecisionResponse> GetKwbDecision([FromQuery(Name = "route")] string route)
    {
        return await timeTableService.GetTransportDecision(route);
    }
}