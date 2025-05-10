using ss_transpo_dss.services.Models;

namespace ss_transpo_dss.services.Interfaces;

public interface ILTADataService
{
    Task<LTABusArrivalModel?> GetBusArrivalsByBusCodeAndServiceNo(string busCode, string? serviceNo);
    Task<List<LTABusServiceRecord>> GetBusArrivalsInMinutesByBusCodeAndServiceNo(string busCode, string? serviceNo);
    
}