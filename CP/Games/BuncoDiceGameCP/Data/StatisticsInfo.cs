namespace BuncoDiceGameCP.Data;
public partial class StatisticsInfo
{
    [LabelGrid]
    public string Turn { get; set; } = "None";
    [LabelGrid]
    public int NumberToGet { get; set; }
    [LabelGrid]
    public int Set { get; set; }
    [LabelGrid]
    public int YourTeam { get; set; }
    [LabelGrid]
    public int YourPoints { get; set; }
    [LabelGrid]
    public int OpponentScore { get; set; }
    [LabelGrid]
    public int Buncos { get; set; }
    [LabelGrid]
    public int Wins { get; set; }
    [LabelGrid]
    public int Losses { get; set; }
    [LabelGrid]
    public int YourTable { get; set; }
    [LabelGrid]
    public string TeamMate { get; set; } = "None";
    [LabelGrid]
    public string Opponent1 { get; set; } = "None";
    [LabelGrid]
    public string Opponent2 { get; set; } = "None";
    [LabelGrid]
    public string Status { get; set; } = "Disconnected";
}
