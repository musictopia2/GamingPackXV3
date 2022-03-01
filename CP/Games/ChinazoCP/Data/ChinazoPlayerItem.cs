namespace ChinazoCP.Data;
[UseScoreboard]
public partial class ChinazoPlayerItem : PlayerRummyHand<ChinazoCard>
{//anything needed is here
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
    [ScoreColumn]
    public bool LaidDown { get; set; }
}