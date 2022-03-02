namespace HitTheDeckCP.Data;
[UseScoreboard]
public partial class HitTheDeckPlayerItem : PlayerSingleHand<HitTheDeckCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int PreviousPoints { get; set; }
    [ScoreColumn]
    public int TotalPoints { get; set; }
}