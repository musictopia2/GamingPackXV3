namespace SavannahCP.Data;
[UseScoreboard]
public partial class SavannahPlayerItem : PlayerSingleHand<RegularSimpleCard>
{//anything needed is here
    [ScoreColumn]
    public int DiscardLeft => DiscardList.Count; //try this way.
    [ScoreColumn]
    public int ReserveLeft => ReserveList.Count;
    public int WhenToStackDiscards { get; set; }
    //hopefully good enough for that part.
    //not sure how this work work for reshuffling cards (?)
    public DeckRegularDict<RegularSimpleCard> ReserveList { get; set; } = new();
    public DeckRegularDict<RegularSimpleCard> DiscardList { get; set; } = new();

    //this is always from discardlist.  will attempt to create something where it will show the last card period.


    //hopefully each player don't have to have their own piles anymore (just self).
    //[JsonIgnore]
    //public SingleObservablePile<RegularSimpleCard>? DiscardPile { get; set; }
}