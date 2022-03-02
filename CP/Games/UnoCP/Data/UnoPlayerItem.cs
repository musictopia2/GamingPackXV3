namespace UnoCP.Data;
[UseScoreboard]
public partial class UnoPlayerItem : PlayerSingleHand<UnoCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int TotalPoints { get; set; }
    [ScoreColumn]
    public int PreviousPoints { get; set; }
}