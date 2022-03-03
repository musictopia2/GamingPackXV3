namespace SixtySix2PlayerCP.Data;
[UseScoreboard]
public partial class SixtySix2PlayerPlayerItem : PlayerTrick<EnumSuitList, SixtySix2PlayerCardInformation>
{//anything needed is here
    [ScoreColumn]
    public int ScoreRound { get; set; }
    [ScoreColumn]
    public int GamePointsRound { get; set; }
    [ScoreColumn]
    public int GamePointsGame { get; set; }
    public EnumMarriage FirstMarriage { get; set; }
    public BasicList<EnumMarriage> MarriageList { get; set; } = new();
}