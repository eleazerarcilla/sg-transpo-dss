using ss_transpo_dss.services.Globals;
using ss_transpo_dss.services.Models;

namespace ss_transpo_dss.services.Helpers;

public static class LogicHelper
{
    public static List<LTABusServiceRecord> ToBusArrivalsRecord(this LTABusArrivalModel? ltaBusArrivalModel)
        =>  (from busService in ltaBusArrivalModel.BusServices
            select new LTABusServiceRecord
            (
                ServiceNo: busService.ServiceNo.LtaBusServiceNo(),
                Arrivals: new()
                {
                    busService?.NextBus.EstimatedArrival.ConvertToDateTime().MinutesFromNow(),
                    busService?.NextBus2.EstimatedArrival.ConvertToDateTime().MinutesFromNow(),
                    busService?.NextBus3.EstimatedArrival.ConvertToDateTime().MinutesFromNow()
                }
            )).ToList();

    private static string LtaBusServiceNo(this string? serviceNo) => serviceNo ?? Constants.NO_TRANSPORT_AVAILABLE;
}