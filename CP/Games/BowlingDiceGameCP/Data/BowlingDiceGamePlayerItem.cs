namespace BowlingDiceGameCP.Data;
public class BowlingDiceGamePlayerItem : SimplePlayer
{ //anything needed is here
    public int TotalScore { get; set; }
    public Dictionary<int, FrameInfoCP> FrameList { get; set; } = new ();
}