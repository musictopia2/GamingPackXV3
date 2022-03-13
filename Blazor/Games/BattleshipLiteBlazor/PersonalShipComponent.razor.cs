namespace BattleshipLiteBlazor;
public partial class PersonalShipComponent
{
    [CascadingParameter]
    private BattleshipLiteMainViewModel? DataContext { get; set; }
    [CascadingParameter]
    private string TargetHeight { get; set; } = "";
    private string PieceColor(ShipInfo ship)
    {
        var opponent = DataContext!.OpponentShip(ship);
        //BasicList<ShipInfo> list = DataContext!.YourPlacedShips();
        if (opponent.ShipStatus == EnumShipStatus.Hit)
        {
            return cc.Red.ToWebColor();
        }
        return cc.Gray.ToWebColor();
    }
}