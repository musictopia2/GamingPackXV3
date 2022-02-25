namespace DeadDie96CP.Data;
[UseScoreboard]
public partial class DeadDie96PlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public int CurrentScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
}