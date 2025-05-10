using Microsoft.AspNetCore.Components;
using sg_transpo_rcl.Clients;
using ss_transpo_dss.services.Models;

namespace sg_transpo_rcl.Base;

public abstract class BusArrivalsBase : ComponentBase, IDisposable
{
    
    protected LTABusArrivalModel _ltaBusArrivalModel;
   
    [Inject] private TransportApiClient TransportApiClient { get; set; } = default!;
    [Parameter][EditorRequired] public string BusStopCode { get; set; } = default!;
    protected override async Task OnInitializedAsync()
    {
        _ltaBusArrivalModel = await TransportApiClient.GetLTABusArrivalsByBusStopCode(BusStopCode);
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}