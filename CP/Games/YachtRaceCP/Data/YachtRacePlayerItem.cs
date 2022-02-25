namespace YachtRaceCP.Data;
[UseScoreboard]
public partial class YachtRacePlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public float Time { get; set; }
}