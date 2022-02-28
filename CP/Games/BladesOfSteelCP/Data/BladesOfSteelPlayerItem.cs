namespace BladesOfSteelCP.Data;
[UseScoreboard]
public partial class BladesOfSteelPlayerItem : PlayerSingleHand<RegularSimpleCard>
{//anything needed is here
    public RegularSimpleCard? FaceOff { get; set; }
    [ScoreColumn]
    public int Score { get; set; }
    public DeckRegularDict<RegularSimpleCard> AttackList { get; set; } = new ();
    public DeckRegularDict<RegularSimpleCard> DefenseList { get; set; } = new ();
    [JsonIgnore]
    public PlayerAttackCP? AttackPile { get; set; }
    [JsonIgnore]
    public PlayerDefenseCP? DefensePile { get; set; }
}