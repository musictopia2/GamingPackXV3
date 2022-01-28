namespace SolitaireBoardGameCP.ViewModels;
[InstanceGame]
public class SolitaireBoardGameMainViewModel : ScreenViewModel, IBasicEnableProcess, IBlankGameVM, IAggregatorContainer
{
    private readonly SolitaireBoardGameMainGameClass _mainGame;
    public SolitaireBoardGameMainViewModel(IEventAggregator aggregator, CommandContainer commandContainer, IGamePackageResolver resolver) : base(aggregator)
    {
        CommandContainer = commandContainer;
        _mainGame = resolver.ReplaceObject<SolitaireBoardGameMainGameClass>();
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