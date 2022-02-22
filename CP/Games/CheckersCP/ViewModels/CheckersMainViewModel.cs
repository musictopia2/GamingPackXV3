namespace CheckersCP.ViewModels;
[InstanceGame]
public class CheckersMainViewModel : BasicMultiplayerMainVM
{
    private readonly CheckersMainGameClass _mainGame; //if we don't need, delete.
    public CheckersVMData VMData { get; set; }
    public CheckersMainViewModel(CommandContainer commandContainer,
        CheckersMainGameClass mainGame,
        BasicData basicData,
        TestOptions test,
        IGamePackageResolver resolver,
        IEventAggregator aggregator,
        CheckersVMData data
        )
        : base(commandContainer, mainGame, basicData, test, resolver, aggregator)
    {
        _mainGame = mainGame;
        VMData = data;
    }
    //anything else needed is here.

}
