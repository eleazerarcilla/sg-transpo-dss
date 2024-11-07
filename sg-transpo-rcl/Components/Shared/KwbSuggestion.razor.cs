using Microsoft.AspNetCore.Components;
using sg_transpo_rcl.Clients;
using ss_transpo_dss.services.Models.Responses;

namespace sg_transpo_rcl.Components.Shared;

public partial class KwbSuggestion : ComponentBase
{
    private DecisionResponse _decisionResponse;
    [Inject] private TransportApiClient TransportApiClient { get; set; } = default!;
    [Parameter][EditorRequired] public string Route { get; set; } = default!;
    protected override async Task OnInitializedAsync()
    {
        _decisionResponse = await TransportApiClient.GetDecisionResponseAsync(Route);
    }
}