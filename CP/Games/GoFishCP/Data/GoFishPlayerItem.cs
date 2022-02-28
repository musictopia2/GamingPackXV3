namespace GoFishCP.Data;
[UseScoreboard]
public partial class GoFishPlayerItem : PlayerSingleHand<RegularSimpleCard>
{//anything needed is here
    [ScoreColumn]
    public int Pairs { get; set; }
}