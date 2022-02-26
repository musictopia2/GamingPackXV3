namespace SequenceDiceCP.Data;
[UseScoreboard]
public partial class SequenceDicePlayerItem : PlayerBoardGame<EnumColorChoice>
{//anything needed is here
    public override bool DidChooseColor => Color.IsNull == false && Color != EnumColorChoice.None;
    public override void Clear()
    {
        Color = EnumColorChoice.None;
    }
}