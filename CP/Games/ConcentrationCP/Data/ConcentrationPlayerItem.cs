namespace ConcentrationCP.Data;
[UseScoreboard]
public partial class ConcentrationPlayerItem : PlayerSingleHand<RegularSimpleCard>
{//anything needed is here
    [ScoreColumn]
    public int Pairs { get; set; }
}