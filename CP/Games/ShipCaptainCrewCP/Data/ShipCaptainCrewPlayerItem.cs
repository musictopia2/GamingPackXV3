namespace ShipCaptainCrewCP.Data;
[UseScoreboard]
public partial class ShipCaptainCrewPlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public bool WentOut { get; set; }
    [ScoreColumn]
    public int Score { get; set; }
    [ScoreColumn]
    public int Wins { get; set; }
    [ScoreColumn]
    public bool TookTurn { get; set; }
}