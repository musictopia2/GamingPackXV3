using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;

namespace OpetongCP.Data;
[SingletonGame]
[AutoReset] //usually needs reset
public class OpetongGameContainer : CardGameContainer<RegularRummyCard, OpetongPlayerItem, OpetongSaveInfo>
{
    public OpetongGameContainer(BasicData basicData,
        TestOptions test,
        IGameInfo gameInfo,
        IAsyncDelayer delay,
        IEventAggregator aggregator,
        CommandContainer command,
        IGamePackageResolver resolver,
        IListShuffler<RegularRummyCard> deckList,
        IRandomGenerator random) : base(basicData, test, gameInfo, delay, aggregator, command, resolver, deckList, random)
    {
        Rummys = new ();
        Rummys.HasSecond = true;
        Rummys.HasWild = false;
        Rummys.LowNumber = 1;
        Rummys.HighNumber = 13;
        Rummys.NeedMatch = false;
    }
    internal RummyProcesses<EnumSuitList, EnumRegularColorList, RegularRummyCard> Rummys { get; set; }
    internal Func<int, Task>? DrawFromPoolAsync { get; set; }
}