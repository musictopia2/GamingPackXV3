namespace MastermindCP.ViewModels;
[InstanceGame]
public class MastermindMainViewModel : ScreenViewModel, IBasicEnableProcess, IBlankGameVM, IAggregatorContainer
{
    private readonly MastermindMainGameClass _mainGame;
    public MastermindMainViewModel(IEventAggregator aggregator, CommandContainer commandContainer, IGamePackageResolver resolver) : base(aggregator)
    {
        CommandContainer = commandContainer;
        _mainGame = resolver.ReplaceObject<MastermindMainGameClass>();
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