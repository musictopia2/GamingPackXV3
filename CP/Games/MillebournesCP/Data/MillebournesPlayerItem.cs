namespace MillebournesCP.Data;
[UseScoreboard]
public partial class MillebournesPlayerItem : PlayerSingleHand<MillebournesCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int Team { get; set; }
    [ScoreColumn]
    public bool OtherTurn { get; set; }
    [ScoreColumn]
    public int Miles { get; set; }
    [ScoreColumn]
    public int OtherPoints { get; set; }
    [ScoreColumn]
    public int TotalPoints { get; set; }
    [ScoreColumn]
    public int Number200s { get; set; }
}