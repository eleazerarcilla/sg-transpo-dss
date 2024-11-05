using System.Text.Json.Serialization;

namespace ss_transpo_dss.services.Models;

public class LTABusArrivalModel
{
    [JsonPropertyName("odata.metadata")]
    public string metadata { get; set; }
    [JsonPropertyName("BusStopCode")]
    public string BusStopCode { get; set; }
    [JsonPropertyName("Services")]
    public List<BusService> BusServices { get; set; }
}

public class BusService()
{
    public string ServiceNo  { get; set; }
    public string Operator { get; set; }
    [JsonPropertyName("NextBus")]
    public BusProperties NextBus { get; set; }
    [JsonPropertyName("NextBus2")]
    public BusProperties NextBus2 { get; set; }
    [JsonPropertyName("NextBus3")]
    public BusProperties NextBus3 { get; set; }
}

public class BusProperties()
{
    public string OriginCode { get; set; }
    public string DestinationCode { get; set; }
    public string? EstimatedArrival { get; set; }
    public int Monitored{ get; set; }
    public string Latitude{ get; set; }
    public string Longitude{ get; set; }
    public string VisitNumber{ get; set; }
    public string Load{ get; set; }
    public string Feature{ get; set; }
    public string Type{ get; set; }
}