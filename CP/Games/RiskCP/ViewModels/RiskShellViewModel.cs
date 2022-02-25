namespace RiskCP.ViewModels;
public class RiskShellViewModel : BasicBoardGamesShellViewModel<RiskPlayerItem>
{
    public RiskShellViewModel(IGamePackageResolver mainContainer,
        CommandContainer container,
        IGameInfo gameData,
        BasicData basicData,
        IMultiplayerSaveState save,
        TestOptions test,
        IEventAggregator aggregator,
        IToast toast
        )
        : base(mainContainer, container, gameData, basicData, save, test, aggregator, toast)
    {
    }
    protected override IMainScreen GetMainViewModel()
    {
        var model = MainContainer.Resolve<RiskMainViewModel>();
        return model;
    }
    protected override async Task ShowNewGameAsync()
    {
        await _message.ShowMessageAsync("Game is over.  However, unable to allow for new game.  If you want new game, close out and reconnect again.");
    }
}