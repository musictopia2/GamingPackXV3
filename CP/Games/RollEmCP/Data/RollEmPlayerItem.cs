namespace RollEmCP.Data;
[UseScoreboard]
public partial class RollEmPlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public int ScoreRound { get; set; }
    [ScoreColumn]
    public int ScoreGame { get; set; }
}