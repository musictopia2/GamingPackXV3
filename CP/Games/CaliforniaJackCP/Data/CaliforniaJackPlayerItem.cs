namespace CaliforniaJackCP.Data;
[UseScoreboard]
public partial class CaliforniaJackPlayerItem : PlayerTrick<EnumSuitList, CaliforniaJackCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int Points { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}