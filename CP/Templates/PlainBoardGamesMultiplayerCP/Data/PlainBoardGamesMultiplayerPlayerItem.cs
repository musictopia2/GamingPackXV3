namespace PlainBoardGamesMultiplayerCP.Data;
public class PlainBoardGamesMultiplayerPlayerItem : PlayerBoardGame<EnumColorChoice>
{ 
    public override bool DidChooseColor => Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
}