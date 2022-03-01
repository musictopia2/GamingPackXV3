namespace DummyRummyCP.Data;
[UseScoreboard]
public partial class DummyRummyPlayerItem : PlayerRummyHand<RegularRummyCard>
{//anything needed is here]
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}