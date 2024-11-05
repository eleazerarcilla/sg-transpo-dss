using ss_transpo_dss.services.Helpers;
using ss_transpo_dss.services.Interfaces;
using ss_transpo_dss.services.Models;
using ss_transpo_dss.clients.apiclient;

namespace ss_transpo_dss.services.Services;

public class LTADataService(ApiClient apiClient) : ILTADataService
{
    public async Task<LTABusArrivalModel> GetBusArrivalsByBusCodeAndServiceNo(string busCode, string serviceNo)
    {
        return await apiClient.GetBusArrival(busCode, serviceNo);
    }

    public async Task<LTABusServiceRecord> GetBusArrivalsInMinutesByBusCodeAndServiceNo(string busCode, string serviceNo)
    {
        var ltaBusTiming = await GetBusArrivalsByBusCodeAndServiceNo(busCode, serviceNo);
        List<int?> ltaBusServiceTimings = new()
        {
            ltaBusTiming.BusServices.FirstOrDefault()?.NextBus.EstimatedArrival.ConvertToDateTime().MinutesFromNow(),
            ltaBusTiming.BusServices.FirstOrDefault()?.NextBus2.EstimatedArrival.ConvertToDateTime().MinutesFromNow(),
            ltaBusTiming.BusServices.FirstOrDefault()?.NextBus3.EstimatedArrival.ConvertToDateTime().MinutesFromNow()
        };

        return new LTABusServiceRecord(ltaBusTiming.BusServices.FirstOrDefault()?.ServiceNo ?? "N/A",
            ltaBusServiceTimings);
    }
}