using System.Net.Http.Json;
using ss_transpo_dss.services.Models;

namespace ss_transpo_dss.clients.apiclient;

public class ApiClient(string baseUri, string licenseKey, HttpClient httpClient)
{
    public async Task<LTABusArrivalModel> GetBusArrival(string busStopId, string serviceNo)
    {
        httpClient.DefaultRequestHeaders.Add("AccountKey", licenseKey);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        return await httpClient.GetFromJsonAsync<LTABusArrivalModel>(requestUri: $"{baseUri}ltaodataservice/v3/BusArrival{buildQuery(busStopId, serviceNo)}");
    }

    private string buildQuery(string busStopId, string serviceNo)
    {
        string queryString = $"?BusStopCode={busStopId}";
        return !string.IsNullOrEmpty(serviceNo) ? $"{queryString}&ServiceNo={serviceNo}" 
            :queryString;
    }
}