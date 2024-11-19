using System.Net.Http.Json;
using ss_transpo_dss.services.Models;
using ss_transpo_dss.services.Models.Responses;

namespace sg_transpo_rcl.Clients;

public class TransportApiClient(string baseUri, HttpClient httpClient)
{
    public async Task<DecisionResponse> GetDecisionResponseAsync(string route)
    {
        return await httpClient.GetFromJsonAsync<DecisionResponse>(requestUri: $"{baseUri}/timing/getKwbDecision?route={route}");
    }
    public async Task<LTABusArrivalModel> GetLTABusArrivalsByBusStopCode(string busStopCode)
    {
        return await httpClient.GetFromJsonAsync<LTABusArrivalModel>(requestUri: $"{baseUri}/timing/getBusArrivalsByBusStopCode?busStopCode={busStopCode}");
    }
}