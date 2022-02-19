namespace ConnectFourCP.Data;
[SingletonGame]
public class ConnectFourGameContainer : BasicGameContainer<ConnectFourPlayerItem, ConnectFourSaveInfo>
{
    public ConnectFourGameContainer(BasicData basicData,
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