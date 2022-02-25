namespace TroubleCP.Data;
[UseScoreboard]
public partial class TroublePlayerItem : PlayerBoardGame<EnumColorChoice>
{//anything needed is here
    public override bool DidChooseColor => Color.IsNull == false && Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
    public BasicList<int> PieceList { get; set; } = new();
}