namespace BackgammonCP.Data;
public partial class BackgammonPlayerItem : PlayerBoardGame<EnumColorChoice>
{//anything needed is here
    public override bool DidChooseColor => Color.IsNull == false && Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
    public BackgammonPlayerDetails? StartTurnData { get; set; }
    public BackgammonPlayerDetails? CurrentTurnData { get; set; }
}