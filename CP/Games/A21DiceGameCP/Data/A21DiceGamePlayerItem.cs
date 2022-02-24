namespace A21DiceGameCP.Data;
[UseScoreboard]
public partial class A21DiceGamePlayerItem : SimplePlayer
{//anything needed is here
    [ScoreColumn]
    public bool IsFaceOff { get; set; }
    [ScoreColumn]
    public int Score { get; set; }
    [ScoreColumn]
    public int NumberOfRolls { get; set; }
}