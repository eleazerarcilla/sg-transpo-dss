using ss_transpo_dss.services.Models;
using ss_transpo_dss.services.Models.Responses;

namespace ss_transpo_dss.services.Interfaces;

public interface ITimeTableService
{
    Task<TimeTableModel> GetTimingsByRoute(string route);
    Task<DateTime?> GetClosestDepartureByRoute(string route);
    Task<DecisionResponse> GetTransportDecision(string route);
}