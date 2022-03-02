namespace TeeItUpCP.Data;
[UseScoreboard]
public partial class TeeItUpPlayerItem : PlayerSingleHand<TeeItUpCardInformation>
{//anything needed is here
    [JsonIgnore]
    public TeeItUpPlayerBoardCP? PlayerBoard { get; set; }
    public bool FinishedChoosing { get; set; }
    [ScoreColumn]
    public bool WentOut { get; set; }
    [ScoreColumn]
    public int PreviousScore { get; set; }
    [ScoreColumn]
    public int TotalScore { get; set; }
    public void LoadPlayerBoard(TeeItUpGameContainer gameContainer)
    {
        PlayerBoard = new TeeItUpPlayerBoardCP(gameContainer);
        PlayerBoard.LoadBoard(this);
    }
}