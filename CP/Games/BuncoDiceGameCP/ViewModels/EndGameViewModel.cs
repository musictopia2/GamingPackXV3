namespace BuncoDiceGameCP.ViewModels;
[InstanceGame]
public partial class EndGameViewModel : ScreenViewModel, IBlankGameVM
{
    private readonly BuncoDiceGameMainGameClass _game;
    [Command(EnumCommandCategory.Plain)]
    public async Task EndGameAsync()
    {
        await _game.PossibleNewGameAsync();
    }
    public EndGameViewModel(CommandContainer container, BuncoDiceGameMainGameClass game, IEventAggregator aggregator) : base(aggregator)
    {
        CommandContainer = container;
        _game = game;
        CommandContainer.ManuelFinish = false;
        CommandContainer.IsExecuting = false;
        CreateCommands(container);
    }
    partial void CreateCommands(CommandContainer container);
    public CommandContainer CommandContainer { get; set; }
}