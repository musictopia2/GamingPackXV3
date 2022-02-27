namespace CrazyEightsCP.Data;
[SingletonGame]
[AutoReset]
public class CrazyEightsGameContainer : CardGameContainer<RegularSimpleCard, CrazyEightsPlayerItem, CrazyEightsSaveInfo>
{
    public CrazyEightsGameContainer(BasicData basicData,
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
}