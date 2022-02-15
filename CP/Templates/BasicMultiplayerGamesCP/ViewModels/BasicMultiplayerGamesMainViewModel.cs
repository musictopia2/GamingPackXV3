namespace BasicMultiplayerGamesCP.ViewModels;
[InstanceGame]
public class BasicMultiplayerGamesMainViewModel : ScreenViewModel, IBasicEnableProcess, IBlankGameVM, IAggregatorContainer
{
    private readonly BasicMultiplayerGamesMainGameClass _mainGame;
    public BasicMultiplayerGamesMainViewModel(IEventAggregator aggregator, CommandContainer commandContainer, IGamePackageResolver resolver) : base(aggregator)
    {
        CommandContainer = commandContainer;
        _mainGame = resolver.ReplaceObject<BasicMultiplayerGamesMainGameClass>();
    }
    public CommandContainer CommandContainer { get; set; }
    IEventAggregator IAggregatorContainer.Aggregator => Aggregator;
    public bool CanEnableBasics()
    {
        return true;
    }
    protected override async Task ActivateAsync()
    {
        await base.ActivateAsync();
        await _mainGame.NewGameAsync();
    }
}