namespace BowlingDiceGameCP.Data;
public class FrameInfoCP
{
    public Dictionary<int, SectionInfoCP> SectionList = new();
    public int Score { get; set; } = -1;
}