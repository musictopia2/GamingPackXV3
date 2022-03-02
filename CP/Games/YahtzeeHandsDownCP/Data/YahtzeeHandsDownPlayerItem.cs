namespace YahtzeeHandsDownCP.Data;
[UseScoreboard]
public partial class YahtzeeHandsDownPlayerItem : PlayerSingleHand<YahtzeeHandsDownCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int TotalScore { get; set; }
    [ScoreColumn]
    public int ScoreRound { get; set; }
    [ScoreColumn]
    public string WonLastRound { get; set; } = "";
    public YahtzeeResults Results { get; set; } = new YahtzeeResults();
}