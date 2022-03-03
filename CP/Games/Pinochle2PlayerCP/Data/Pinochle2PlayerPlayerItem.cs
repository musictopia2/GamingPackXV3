namespace Pinochle2PlayerCP.Data;
[UseScoreboard]
public partial class Pinochle2PlayerPlayerItem : PlayerTrick<EnumSuitList, Pinochle2PlayerCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}