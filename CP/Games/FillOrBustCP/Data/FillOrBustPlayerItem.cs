namespace FillOrBustCP.Data;
[UseScoreboard]
public partial class FillOrBustPlayerItem : PlayerSingleHand<FillOrBustCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}