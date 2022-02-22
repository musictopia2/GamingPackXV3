namespace CheckersCP.Data;
[SingletonGame]
public class CheckersGameContainer : BasicGameContainer<CheckersPlayerItem, CheckersSaveInfo>
{
    public CheckersGameContainer(BasicData basicData,
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