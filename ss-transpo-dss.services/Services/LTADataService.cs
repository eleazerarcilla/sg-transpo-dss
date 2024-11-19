using ss_transpo_dss.services.Helpers;
using ss_transpo_dss.services.Interfaces;
using ss_transpo_dss.services.Models;
using ss_transpo_dss.clients.apiclient;

namespace ss_transpo_dss.services.Services;

public class LTADataService(ApiClient apiClient) : ILTADataService
{
    public async Task<LTABusArrivalModel?> GetBusArrivalsByBusCodeAndServiceNo(string busCode, string? serviceNo) 
        => await apiClient.GetBusArrival(busCode, serviceNo);
    
    public async Task<List<LTABusServiceRecord>> GetBusArrivalsInMinutesByBusCodeAndServiceNo(string busCode,
        string? serviceNo)
        => (await GetBusArrivalsByBusCodeAndServiceNo(busCode, serviceNo)).ToBusArrivalsRecord();
}