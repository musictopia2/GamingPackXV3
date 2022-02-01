namespace BuncoDiceGameCP.Data;
[SingletonGame]
//[SourceGeneratedSerialization] //because of the playercollection and dicelist problem cannot do the source generated way.  besides, no autoresume anyways for now
public class BuncoDiceGameSaveInfo : IMappable
{
    public PlayerCollection<PlayerItem> PlayerList { get; set; } = new PlayerCollection<PlayerItem>();
    public StatisticsInfo ThisStats { get; set; } = new StatisticsInfo();
    public DiceList<SimpleDice> DiceList { get; set; } = new DiceList<SimpleDice>();//i think
    public PlayOrderClass? PlayOrder; //i think
    public int WhatSet { get; set; }
    public int WhatNumber { get; set; }
    public bool DidHaveBunco { get; set; }
    public bool SameTable { get; set; }
    public int TurnNum { get; set; } //i think this is still needed.
    public bool HadBunco { get; set; }
    public bool HasRolled { get; set; }
    public bool MaxedRolls { get; set; }
}