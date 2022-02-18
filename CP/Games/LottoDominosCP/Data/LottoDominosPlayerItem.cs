namespace LottoDominosCP.Data;
[UseScoreboard]
public partial class LottoDominosPlayerItem : SimplePlayer
{ //anything needed is here
    [ScoreColumn]
    public int NumberChosen { get; set; } = -1;
    [ScoreColumn]
    public int NumberWon { get; set; }
}