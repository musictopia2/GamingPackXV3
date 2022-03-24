namespace SavannahCP.Data;
[SingletonGame]
[AutoReset] //usually needs reset
public class SavannahGameContainer : CardGameContainer<RegularSimpleCard, SavannahPlayerItem, SavannahSaveInfo>
{
    public SavannahGameContainer(BasicData basicData,
        TestOptions test,
        IGameInfo gameInfo,
        IAsyncDelayer delay,
        IEventAggregator aggregator,
        CommandContainer command,
        IGamePackageResolver resolver,
        IListShuffler<RegularSimpleCard> deckList,
        IRandomGenerator random) : base(basicData, test, gameInfo, delay, aggregator, command, resolver, deckList, random)
    {
    }
    public int DeckCount => 52 * PlayerList!.Count;
    public Func<Task>? UnselectAllPilesAsync { get; set; } //the main game will do this.
    public Func<Task>? DiscardAsync { get; set; }
}