namespace YaBlewItCP.Data;
[UseScoreboard]
public partial class YaBlewItPlayerItem : PlayerSingleHand<YaBlewItCardInformation>
{//anything needed is here
    [ScoreColumn]
    public EnumColors CursedGem { get; set; }
    [ScoreColumn]
    public int TotalScore => MainHandList.TotalPoints(this);
}