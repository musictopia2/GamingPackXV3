namespace A8RoundRummyCP.Data;
[UseScoreboard]
public partial class A8RoundRummyPlayerItem : PlayerSingleHand<A8RoundRummyCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int TotalScore { get; set; }
}