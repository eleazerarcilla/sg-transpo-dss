using Microsoft.AspNetCore.Components;

namespace sg_transpo_dss.Client.Pages;

public partial class Manual
{
    //[SupplyParameterFromQuery(Name = "BusStopCode")]
    [Parameter]public string BusStopCode { get; set; } = default!;
    public void ChangeBusStopCode() => StateHasChanged();
}