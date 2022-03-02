namespace DutchBlitzCP.Data;
[UseScoreboard]
public partial class DutchBlitzPlayerItem : PlayerSingleHand<DutchBlitzCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int StockLeft { get; set; }
    [ScoreColumn]
    public int PointsRound { get; set; }
    [ScoreColumn]
    public int PointsGame { get; set; }
}