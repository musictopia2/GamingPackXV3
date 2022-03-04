namespace SnagCardGameCP.Data;
[UseScoreboard]
public partial class SnagCardGamePlayerItem : PlayerTrick<EnumSuitList, SnagCardGameCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int CardsWon { get; set; }
    [ScoreColumn]
    public int CurrentPoints { get; set; }
    [ScoreColumn]
    public int TotalPoints { get; set; }
}