using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using static ss_transpo_dss.services.Globals.Constants;

namespace ss_transpo_dss.services.Models;

public class TimeTableModel
{
    [JsonPropertyName("Route")] public string Route { get; set; }
    [IgnoreDataMember]
    [JsonPropertyName("Timings")] public string[] Timings { get; set; }

    public List<DateTime> TimingDates
    {
        get
        {
            return Timings.Select(t => DateTime.ParseExact(t, KWB_DATETIME_FORMAT, CultureInfo.InvariantCulture)).ToList();
        }
    }
}