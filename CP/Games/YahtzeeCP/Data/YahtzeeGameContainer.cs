namespace YahtzeeCP.Data;
[SingletonGame]
public class YahtzeeGameContainer : BasicGameContainer<YahtzeePlayerItem, YahtzeeSaveInfo>
{
    public YahtzeeGameContainer(BasicData basicData,
        TestOptions test,
        IGameInfo gameInfo,
        IAsyncDelayer delay,
        IEventAggregator aggregator,
        CommandContainer command,
        IGamePackageResolver resolver,
        IRandomGenerator random) : base(basicData, test, gameInfo, delay, aggregator, command, resolver, random)
    {
    }
}