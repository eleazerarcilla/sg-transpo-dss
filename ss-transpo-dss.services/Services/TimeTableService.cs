using System.Text.Json;
using ss_transpo_dss.services.Globals;
using ss_transpo_dss.services.Interfaces;
using ss_transpo_dss.services.Models;
using ss_transpo_dss.services.Models.Responses;
using static ss_transpo_dss.services.Globals.Constants;
using static ss_transpo_dss.services.Helpers.DecisionHelpers;
using ss_transpo_dss.clients.apiclient;

namespace ss_transpo_dss.services.Services;

public sealed class TimeTableService(ApiClient apiClient, ILTADataService ltaDataService) : ITimeTableService
{
    private ApiClient _client = apiClient;

    public async Task<TimeTableModel> GetTimingsByRoute(string route)
    {
        string kwbRoute = GetKwbRoute(route);
        var timings = await LoadKwbTimingsJson();
        return timings.FirstOrDefault(key => key.Route == kwbRoute) ?? new();
    }

    public async Task<DateTime?> GetClosestDepartureByRoute(string route)
    {
        DateTime now = DateTime.Now; //SGT
        string kwbRoute = GetKwbRoute(route);
        var timings = await LoadKwbTimingsJson();
        
        if(timings.Any(key => key.Route == kwbRoute) == false) return null;
        
        var routeTimings = timings.FirstOrDefault(key => key.Route == kwbRoute) ?? new();
        var minDate = routeTimings.TimingDates.Where(x=> x >= now).OrderBy(x => Math.Abs((now - x).TotalSeconds)).FirstOrDefault();
        return minDate;
    }

    public async Task<DecisionResponse> GetTransportDecision(string route)
    {
        int shuttleTiming = (await GetClosestDepartureByRoute(route)).MinutesFromNow();
        var ltaBusTiming = await ltaDataService.GetBusArrivalsInMinutesByBusCodeAndServiceNo(route.KwbRouteBusStopCode(),
                KwbRouteModes.BUSSERVICENO);
        bool takeBus = ltaBusTiming.Arrivals.FirstOrDefault() <= shuttleTiming;
        string transportMode = takeBus ? TransportModes.BUS : TransportModes.SHUTTLE;
        var arrivals = takeBus ? ltaBusTiming.Arrivals.GetArrivalListString()
            : new(){shuttleTiming.ToString()};
        string otherTransport = takeBus ? TransportModes.SHUTTLE : TransportModes.BUS;
        var otherArrivals = takeBus ? new(){shuttleTiming.ToString()} : ltaBusTiming.Arrivals.GetArrivalListString() ;
        return new DecisionResponse(transportMode, arrivals, otherTransport, otherArrivals);
    }

    private async Task<List<TimeTableModel>> LoadKwbTimingsJson()
    {
        var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, KWB_TIMETABLE_FILENAME));
        return JsonSerializer.Deserialize<List<TimeTableModel>>(json) ?? new List<TimeTableModel>();
    }
}