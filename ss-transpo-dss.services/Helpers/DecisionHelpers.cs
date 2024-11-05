
using System.Globalization;
using ss_transpo_dss.services.Globals;

namespace ss_transpo_dss.services.Helpers;

public static class DecisionHelpers
{
    //Kwb = Kingsford Waterbay
    public static string GetKwbRoute(string? routeString) => routeString?.ToLower() switch
    {
        "incoming" => $"{GetKwbRoutePrefix}-{KwbRouteModes.INCOMING}",
        "outgoing" => $"{GetKwbRoutePrefix}-{KwbRouteModes.OUTGOING}",
        _ => $"{GetKwbRoutePrefix}-{KwbRouteModes.OUTGOING}"
    };
    public static string KwbRouteBusStopCode(this string? routeString) => routeString?.ToLower() switch {
        "incoming" => KwbRouteModes.NEARESTINTERCHANGE_BUSSTOPCODE,
        "outgoing" => KwbRouteModes.NEARESTBUSSTOPCODE,
        _ => KwbRouteModes.NEARESTBUSSTOPCODE
    };
    public static string GetKwbRoutePrefix => IsWeekEnd ? KwbRouteModes.WEEKEND : KwbRouteModes.WEEKDAY;
    public static bool IsWeekEnd => DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;

    public static DateTime? ConvertToDateTime(this string? dateString) => !string.IsNullOrEmpty(dateString)
        ? DateTime.ParseExact(dateString!, Constants.LTA_DATETIME, CultureInfo.InvariantCulture)
        : null;
    
    public static int MinutesFromNow(this DateTime? dateString) => dateString is not null ? 
         Convert.ToInt32(TimeSpan.FromTicks(dateString.Value.Ticks -DateTime.Now.Ticks).TotalMinutes) : 0;

    public static List<string> GetArrivalListString(this List<int?> arrivals)
    {
        List<string> arrivalStrings = new();
        arrivals.OrderBy(x=>x.Value).ToList().ForEach(arrival =>
        {
            arrivalStrings.Add(arrival <= 0 ? "Arr" : arrival.ToString());
        });
        return arrivalStrings;
    }
}