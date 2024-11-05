using System.Net.Http.Json;
using ss_transpo_dss.services.Models.Responses;

namespace sg_transpo_rcl.Clients;

public class TransportApiClient(string baseUri, HttpClient httpClient)
{
    public async Task<DecisionResponse> GetDecisionResponseAsync(string route)
    {
        return await httpClient.GetFromJsonAsync<DecisionResponse>(requestUri: $"{baseUri}/timing/getKwbDecision?route={route}");
    }
}