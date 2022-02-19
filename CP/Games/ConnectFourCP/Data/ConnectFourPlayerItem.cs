namespace ConnectFourCP.Data;
public class ConnectFourPlayerItem : PlayerBoardGame<EnumColorChoice>
{ 
    public override bool DidChooseColor => Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
}