namespace BattleshipBlazor;
public partial class ShipControlBlazor
{
    [Parameter]
    public BasicList<ShipInfoCP>? Ships { get; set; }
    [Parameter]
    public string TargetHeight { get; set; } = "";
}