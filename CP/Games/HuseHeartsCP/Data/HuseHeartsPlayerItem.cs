namespace HuseHeartsCP.Data;
[UseScoreboard]
public partial class HuseHeartsPlayerItem : PlayerTrick<EnumSuitList, HuseHeartsCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int PreviousScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
    [ScoreColumn]
    public bool HadPoints { get; set; }
    public BasicList<int> CardsPassed { get; set; } = new();
}