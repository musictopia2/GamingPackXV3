namespace MonopolyCardGameCP.Data;
[UseScoreboard]
public partial class MonopolyCardGamePlayerItem : PlayerSingleHand<MonopolyCardGameCardInformation>
{//anything needed is here
    [JsonIgnore]
    public TradePile? TradePile { get; set; }
    [ScoreColumn]
    public string TradeString { get; set; } = "";
    [ScoreColumn]
    public decimal PreviousMoney { get; set; }
    [ScoreColumn]
    public decimal TotalMoney { get; set; }
}