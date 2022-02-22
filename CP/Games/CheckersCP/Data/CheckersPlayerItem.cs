namespace CheckersCP.Data;
public class CheckersPlayerItem : PlayerBoardGame<EnumColorChoice>
{ 
    public override bool DidChooseColor => Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
}