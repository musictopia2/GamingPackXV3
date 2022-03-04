namespace SkuckCardGameCP.Data;
[UseScoreboard]
public partial class SkuckCardGamePlayerItem : PlayerTrick<EnumSuitList, SkuckCardGameCardInformation>
{//anything needed is here
    public int StrengthHand { get; set; }
    public string TieBreaker { get; set; } = "";
    public int BidAmount { get; set; }
    public bool BidVisible { get; set; }
    public int PerfectRounds { get; set; }
    public int TotalScore { get; set; }
    [JsonIgnore]
    public PlayerBoardObservable<SkuckCardGameCardInformation>? TempHand { get; set; }

    public DeckRegularDict<SkuckCardGameCardInformation> SavedTemp = new();
}