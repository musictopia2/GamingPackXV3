namespace SkuckCardGameCP.Data;
[UseScoreboard]
public partial class SkuckCardGamePlayerItem : PlayerTrick<EnumSuitList, SkuckCardGameCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int StrengthHand { get; set; }
    [ScoreColumn]
    public string TieBreaker { get; set; } = "";
    [ScoreColumn]
    public int BidAmount { get; set; }
    [ScoreColumn]
    public bool BidVisible { get; set; }
    [ScoreColumn]
    public int PerfectRounds { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
    [JsonIgnore]
    public PlayerBoardObservable<SkuckCardGameCardInformation>? TempHand { get; set; }

    public DeckRegularDict<SkuckCardGameCardInformation> SavedTemp { get; set; } = new();
}