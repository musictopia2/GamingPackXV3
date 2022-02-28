namespace OpetongCP.Data;
[UseScoreboard]
public partial class OpetongPlayerItem : PlayerRummyHand<RegularRummyCard>
{//anything needed is here
    [ScoreColumn]
    public int SetsPlayed { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}