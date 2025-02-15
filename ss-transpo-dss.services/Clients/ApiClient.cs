using System.Net.Http.Json;
using ss_transpo_dss.services.Models;
using Microsoft.Extensions.Logging;

namespace ss_transpo_dss.clients.apiclient;

public class ApiClient(string baseUri, string licenseKey, HttpClient httpClient, ILogger<ApiClient> logger)
{
    public async Task<LTABusArrivalModel?> GetBusArrival(string busStopId, string? serviceNo)
    {
        logger.LogInformation($"----- GetBusArrival - {busStopId} - {serviceNo}");
        try
        {
            logger.LogInformation($"License Key: {licenseKey}");
            logger.LogInformation($"httpClient is null?: {httpClient == null}");
            logger.LogInformation($"Query String: {buildQuery(busStopId, serviceNo)}");
            logger.LogInformation($"{baseUri}ltaodataservice/v3/BusArrival{buildQuery(busStopId, serviceNo)}");
            
            httpClient.DefaultRequestHeaders.Add("AccountKey", licenseKey);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var s = await httpClient.GetStringAsync(
                requestUri: $"{baseUri}ltaodataservice/v3/BusArrival{buildQuery(busStopId, serviceNo)}");
            
            logger.LogInformation(s);
            
            return await httpClient.GetFromJsonAsync<LTABusArrivalModel>(
                requestUri: $"{baseUri}ltaodataservice/v3/BusArrival{buildQuery(busStopId, serviceNo)}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            logger.LogError(ex.StackTrace);
            logger.LogError(ex.InnerException?.Message);
            return new();
        }
    }

    private string buildQuery(string busStopId, string? serviceNo)
    {
        string queryString = $"?BusStopCode={busStopId}";
        return !string.IsNullOrEmpty(serviceNo) ? $"{queryString}&ServiceNo={serviceNo}" 
            :queryString;
    }
}