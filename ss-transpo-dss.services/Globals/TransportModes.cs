namespace ss_transpo_dss.services.Globals;

public static class TransportModes
{
    public static string SHUTTLE = "Shuttle";
    public static string BUS = "Bus";
    
    public static List<string> ToList() => new() {SHUTTLE, BUS};
}