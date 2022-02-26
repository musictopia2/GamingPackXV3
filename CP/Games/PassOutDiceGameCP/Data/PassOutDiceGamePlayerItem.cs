namespace PassOutDiceGameCP.Data;
public partial class PassOutDiceGamePlayerItem : PlayerBoardGame<EnumColorChoice>
{//anything needed is here
    public override bool DidChooseColor => Color.IsNull == false && Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
}