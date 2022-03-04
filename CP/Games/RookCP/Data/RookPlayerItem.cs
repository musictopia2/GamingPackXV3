namespace RookCP.Data;
[UseScoreboard]
public partial class RookPlayerItem : PlayerTrick<EnumColorTypes, RookCardInformation>
{//anything needed is here
    public bool IsDummy { get; set; }
    public bool Pass { get; set; }
    [ScoreColumn]
    public int BidAmount { get; set; }
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}