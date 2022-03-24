namespace SavannahCP.Data;
[SingletonGame]
public class SavannahSaveInfo : BasicSavedCardClass<SavannahPlayerItem, RegularSimpleCard>, IMappable, ISaveInfo
{
	public DiceList<SimpleDice> DiceList { get; set; } = new();
    public bool ChoseOtherPlayer { get; set; }
    public BasicList<BasicPileInfo<RegularSimpleCard>> PublicPileList { get; set; } = new(); //i think this is fine.  using the list of basic pile information could be fine like skipbo
}