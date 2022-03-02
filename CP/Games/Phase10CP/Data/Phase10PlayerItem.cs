namespace Phase10CP.Data;
[UseScoreboard]
public partial class Phase10PlayerItem : PlayerRummyHand<Phase10CardInformation>
{//anything needed is here
    [ScoreColumn]
    public int Phase { get; set; } = 1;
    [ScoreColumn]
    public bool Completed { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}