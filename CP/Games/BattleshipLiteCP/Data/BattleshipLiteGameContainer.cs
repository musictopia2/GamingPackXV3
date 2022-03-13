namespace BattleshipLiteCP.Data;
[SingletonGame]
[AutoReset]
public class BattleshipLiteGameContainer : BasicGameContainer<BattleshipLitePlayerItem, BattleshipLiteSaveInfo>
{
    public BattleshipLiteGameContainer(BasicData basicData,
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