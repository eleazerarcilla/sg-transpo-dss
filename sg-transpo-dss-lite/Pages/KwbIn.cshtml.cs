using Microsoft.AspNetCore.Mvc.RazorPages;
using sg_transpo_rcl.Clients;
using ss_transpo_dss.services.Globals;
using ss_transpo_dss.services.Models.Responses;

namespace sg_transpo_dss_lite.Pages
{
    public class KwbInModel : PageModel
    {
        private readonly ILogger<KwbInModel> _logger;
        protected TransportApiClient _transportApiClient;
        public Dictionary<string, DecisionResponse> DecisionResponses { get; set; } = new();

        public KwbInModel(ILogger<KwbInModel> logger, TransportApiClient transportApiClient)
        {
            _logger = logger;
            _transportApiClient = transportApiClient;
        }

        public void OnGet()
        {
            DecisionResponses.TryAdd(KwbRouteDirections.INCOMING, GetKwbResult(KwbRouteDirections.INCOMING));
        }
        public DecisionResponse GetKwbResult(string direction)
        {
            return _transportApiClient.GetDecisionResponseAsync(direction).Result;
        }
    }
}
