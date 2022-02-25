namespace ThinkTwiceCP.Data;
[UseScoreboard]
public partial class ThinkTwicePlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public int ScoreRound { get; set; }
    [ScoreColumn]
    public int ScoreGame { get; set; }
}