namespace SavannahCP.ViewModels;
[InstanceGame]
public partial class SavannahMainViewModel : BasicCardGamesVM<RegularSimpleCard>
{
    private readonly SavannahMainGameClass _mainGame; //if we don't need, delete.
    private readonly SavannahGameContainer _gameContainer;
    public SavannahVMData VMData { get; set; }
    public SavannahMainViewModel(CommandContainer commandContainer,
        SavannahMainGameClass mainGame,
        SavannahVMData viewModel,
        BasicData basicData,
        TestOptions test,
        IGamePackageResolver resolver,
        IEventAggregator aggregator,
        SavannahGameContainer gameContainer,
        IToast toast
        )
        : base(commandContainer, mainGame, viewModel, basicData, test, resolver, aggregator, toast)
    {
        _mainGame = mainGame;
        VMData = viewModel;
        _gameContainer = gameContainer;
        VMData.Deck1.NeverAutoDisable = true;
        VMData.PlayerHand1.Maximum = 4;
        CreateCommands(commandContainer);
        VMData.PlayerHand1.BeforeAutoSelectObjectAsync += PlayerHand1_BeforeAutoSelectObjectAsync;
        VMData.SelfStock.StockClickedAsync += SelfStock_StockClickedAsync;
    }
    private async Task SelfStock_StockClickedAsync()
    {
        int nums = VMData.SelfStock.CardSelected();
        if (nums > 0)
        {
            VMData.SelfStock.UnselectCard();
            return;
        }
        await UnselectAllAsync();
        VMData.SelfStock.SelectCard();
    }
    private async Task PlayerHand1_BeforeAutoSelectObjectAsync()
    {
        if (VMData.PlayerHand1.HasSelectedObject())
        {
            //if you selected something, then do nothing (because will do automatically anyways).
            return; //if you selected something, then just unselect period.
        }
        await UnselectAllAsync();
    }
    private async Task UnselectAllAsync()
    {
        if (_gameContainer.UnselectAllPilesAsync is null)
        {
            throw new CustomBasicException("Nobody is handling the UnselectAllPilesAsync");
        }
        await _gameContainer.UnselectAllPilesAsync.Invoke(); //hopefully nothing else because the proper one should be done automatically here.
    }
    protected override Task TryCloseAsync()
    {
        VMData.PlayerHand1.BeforeAutoSelectObjectAsync -= PlayerHand1_BeforeAutoSelectObjectAsync;
        VMData.SelfStock.StockClickedAsync -= SelfStock_StockClickedAsync;
        return base.TryCloseAsync();
    }
    partial void CreateCommands(CommandContainer command);
    [Command(EnumCommandCategory.Game)]
    public async Task ClickPlayerDiscardAsync(SavannahPlayerItem player)
    {
        if (player is null)
        {

        }
        await Task.CompletedTask; //for now.
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
    public bool CanEnablePlayer
    {
        get
        {
            if (_mainGame.SaveRoot.ChoseOtherPlayer)
            {
                return false;
            }
            if (CommandContainer.IsExecuting)
            {
                return false;
            }
            if (CommandContainer.Processing)
            {
                return false;
            }
            //hopefully this simple (?)
            return VMData.Cup!.DiceList.First().Value == VMData.Cup.DiceList.Last().Value;
        }
    }
}