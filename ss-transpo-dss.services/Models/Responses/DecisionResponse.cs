namespace ss_transpo_dss.services.Models.Responses;

public record DecisionResponse(string SuggestedTransport, 
    List<string> NextTimings, 
    string OtherOptions, 
    List<string> OtherOptionTimings);