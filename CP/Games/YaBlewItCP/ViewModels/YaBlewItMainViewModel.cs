namespace YaBlewItCP.ViewModels;
[InstanceGame]
public class YaBlewItMainViewModel : BasicCardGamesVM<YaBlewItCardInformation>
{
    private readonly YaBlewItMainGameClass _mainGame; //if we don't need, delete.
    public YaBlewItVMData VMData { get; set; }
    public YaBlewItMainViewModel(CommandContainer commandContainer,
        YaBlewItMainGameClass mainGame,
        YaBlewItVMData viewModel,
        BasicData basicData,
        TestOptions test,
        IGamePackageResolver resolver,
        IEventAggregator aggregator,
        IToast toast
        )
        : base(commandContainer, mainGame, viewModel, basicData, test, resolver, aggregator, toast)
    {
        _mainGame = mainGame;
        VMData = viewModel;
    }
    //anything else needed is here.
    //if i need something extra, will add to template as well.
    protected override bool CanEnableDeck()
    {
        //todo:  decide whether to enable deck.
        return false; //otherwise, can't compile.
    }
    protected override bool CanEnablePile1()
    {
        //todo:  decide whether to enable deck.
        return false; //otherwise, can't compile.
    }
    protected override async Task ProcessDiscardClickedAsync()
    {
        //if we have anything, will be here.
        await Task.CompletedTask;
    }
    public override bool CanEnableAlways()
    {
        return true;
    }
}