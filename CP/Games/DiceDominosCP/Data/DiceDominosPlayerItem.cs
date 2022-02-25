namespace DiceDominosCP.Data;
[UseScoreboard]
public partial class DiceDominosPlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public int DominosWon { get; set; }
}