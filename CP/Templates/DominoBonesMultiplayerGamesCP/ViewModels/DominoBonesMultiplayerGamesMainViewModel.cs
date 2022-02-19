namespace DominoBonesMultiplayerGamesCP.ViewModels;
[InstanceGame]
public class DominoBonesMultiplayerGamesMainViewModel : BasicMultiplayerMainVM
{
    private readonly DominoBonesMultiplayerGamesMainGameClass _mainGame; //if we don't need, delete.
    public DominoBonesMultiplayerGamesVMData VMData { get; set; }
    public DominoBonesMultiplayerGamesMainViewModel(CommandContainer commandContainer,
        DominoBonesMultiplayerGamesMainGameClass mainGame,
        BasicData basicData,
        TestOptions test,
        IGamePackageResolver resolver,
        IEventAggregator aggregator,
        DominoBonesMultiplayerGamesVMData data
        )
        : base(commandContainer, mainGame, basicData, test, resolver, aggregator)
    {
        _mainGame = mainGame;
        VMData = data;
    }
    //anything else needed is here.

}
