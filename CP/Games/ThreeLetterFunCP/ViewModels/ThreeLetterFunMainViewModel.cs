namespace ThreeLetterFunCP.ViewModels;
[InstanceGame]
public class ThreeLetterFunMainViewModel : BasicMultiplayerMainVM
{
    private readonly ThreeLetterFunMainGameClass _mainGame; //if we don't need, delete.
    public ThreeLetterFunVMData VMData { get; set; }
    public ThreeLetterFunMainViewModel(CommandContainer commandContainer,
        ThreeLetterFunMainGameClass mainGame,
        BasicData basicData,
        TestOptions test,
        IGamePackageResolver resolver,
        IEventAggregator aggregator,
        ThreeLetterFunVMData data
        )
        : base(commandContainer, mainGame, basicData, test, resolver, aggregator)
    {
        _mainGame = mainGame;
        VMData = data;
    }
    //anything else needed is here.

}
