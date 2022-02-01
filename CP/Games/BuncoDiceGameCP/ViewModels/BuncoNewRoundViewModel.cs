namespace BuncoDiceGameCP.ViewModels;
[InstanceGame]
public partial class BuncoNewRoundViewModel : ScreenViewModel, IBlankGameVM
{
    public BuncoNewRoundViewModel(CommandContainer commandContainer, IEventAggregator aggregator) : base(aggregator)
    {
        CommandContainer = commandContainer;
        commandContainer.ManuelFinish = false;
        commandContainer.IsExecuting = false;
        CreateCommands(commandContainer);
    }
    partial void CreateCommands(CommandContainer container);
    public CommandContainer CommandContainer { get; set; }
    [Command(EnumCommandCategory.Plain)]
    public async Task NewRoundAsync()
    {
        await Aggregator.PublishAsync(new ChoseNewRoundEventModel());
    }
}