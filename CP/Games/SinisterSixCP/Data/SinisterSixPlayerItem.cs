namespace SinisterSixCP.Data;
[UseScoreboard]
public partial class SinisterSixPlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public int Score { get; set; }
}