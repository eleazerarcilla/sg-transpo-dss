using System.Diagnostics;
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
        var timings = await GetTimingsByRoute(route);
        var minDate = timings.TimingDates.Where(x=> x >= now).OrderBy(x => Math.Abs((now - x).TotalSeconds)).FirstOrDefault();
        return minDate;
    }

    public async Task<DecisionResponse> GetTransportDecision(string route)
    {
        int shuttleTiming = (await GetClosestDepartureByRoute(route)).MinutesFromNow();
        var ltaBusTiming = await ltaDataService.GetBusArrivalsInMinutesByBusCodeAndServiceNo(route.KwbRouteBusStopCode(),
                KwbRouteModes.BUSSERVICENO);
        bool takeBus = ltaBusTiming.Arrivals.FirstOrDefault() <= shuttleTiming || shuttleTiming < 0;
        
        KeyValuePair<string, List<string>> primarySuggestions = GetDecision(shuttleTiming, ltaBusTiming, takeBus);
        KeyValuePair<string, List<string>> secondarySuggestion = GetDecision(shuttleTiming, ltaBusTiming, !takeBus);
        
        return new DecisionResponse(
            primarySuggestions.Key, 
            primarySuggestions.Value, 
            secondarySuggestion.Key, 
            secondarySuggestion.Value);
    }

    private async Task<List<TimeTableModel>> LoadKwbTimingsJson()
    {
        var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, KWB_TIMETABLE_FILENAME));
        return JsonSerializer.Deserialize<List<TimeTableModel>>(json) ?? new();
    }

    private KeyValuePair<string, List<string>> GetDecision(int shuttleTiming,
        LTABusServiceRecord ltaBusTimings, bool takeBus = false)
        => takeBus ? new KeyValuePair<string, List<string>>($"{TransportModes.BUS.Simplify()}-{ltaBusTimings.ServiceNo}", ltaBusTimings.Arrivals.GetArrivalListString())
                : new KeyValuePair<string, List<string>>(TransportModes.SHUTTLE, new List<string>(){shuttleTiming.GetKwbShuttleTimingString()});
            
    
}