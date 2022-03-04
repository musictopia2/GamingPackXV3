namespace RoundsCardGameCP.ViewModels;
[InstanceGame]
public class RoundsCardGameMainViewModel : BasicCardGamesVM<RoundsCardGameCardInformation>
{
    private readonly RoundsCardGameVMData _model;
    public RoundsCardGameMainViewModel(CommandContainer commandContainer,
        RoundsCardGameMainGameClass mainGame,
        RoundsCardGameVMData viewModel,
        BasicData basicData,
        TestOptions test,
        IGamePackageResolver resolver,
        RoundsCardGameGameContainer gameContainer,
        IEventAggregator aggregator,
        IToast toast
        )
        : base(commandContainer, mainGame, viewModel, basicData, test, resolver, aggregator, toast)
    {
        _model = viewModel;
        _model.Deck1.NeverAutoDisable = true;
    }
    protected override bool CanEnableDeck()
    {
        return false;
    }

    protected override bool CanEnablePile1()
    {
        return false;
    }
    protected override async Task ProcessDiscardClickedAsync()
    {
        await Task.CompletedTask;
    }
    public override bool CanEnableAlways()
    {
        return true;
    }
}