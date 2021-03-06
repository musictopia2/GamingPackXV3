namespace SavannahCP.ViewModels;
[InstanceGame]
public partial class SavannahMainViewModel : BasicCardGamesVM<RegularSimpleCard>
{
    private readonly SavannahMainGameClass _mainGame; //if we don't need, delete.
    private readonly SavannahGameContainer _gameContainer;
    private readonly IToast _toast;
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
        _toast = toast;
        VMData.Deck1.NeverAutoDisable = true;
        VMData.PlayerHand1.Maximum = 4;
        CreateCommands(commandContainer);
        VMData.PlayerHand1.BeforeAutoSelectObjectAsync += PlayerHand1_BeforeAutoSelectObjectAsync;
        VMData.SelfStock.StockClickedAsync += SelfStock_StockClickedAsync;
        VMData.PublicPiles.PileClickedAsync += PublicPiles_PileClickedAsync;
    }

    private async Task PublicPiles_PileClickedAsync(int index, BasicPileInfo<RegularSimpleCard> thisPile)
    {
        SendPlay plays = _mainGame.CardSelected(message =>
        {
            _toast.ShowUserErrorToast(message);
        });
        await Task.Delay(0); //for now.
        if (plays.Deck == 0)
        {
            return;
        }
        var card = _gameContainer.DeckList.GetSpecificItem(plays.Deck);
        int rolled = VMData.Cup!.DiceList.Sum(x => x.Value);
        if (VMData.PublicPiles.CanPlayOnPile(index, rolled, card ) == false)
        {
            _toast.ShowUserErrorToast("Illegal Move");
            return;
        }
        plays.Pile = index;
        if (_gameContainer.BasicData.MultiPlayer)
        {
            await _gameContainer.Network!.SendAllAsync(nameof(IMultiplayerModel.Play), plays);
        }
        await _mainGame.PlayOnPileAsync(plays);
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
        VMData.PublicPiles.PileClickedAsync -= PublicPiles_PileClickedAsync;
        return base.TryCloseAsync();
    }
    partial void CreateCommands(CommandContainer command);
    [Command(EnumCommandCategory.Game)]
    public async Task ClickPlayerDiscardAsync(SavannahPlayerItem player)
    {
        if (player is null)
        {
            throw new CustomBasicException("There was no player");
        }
        if (player.DiscardList.Last().IsSelected)
        {
            player.DiscardList.Last().IsSelected = false;
            return;
        }
        await UnselectAllAsync();
        player.DiscardList.Last().IsSelected = true; //i think.  this is all that is needed.  because you can choose the card.  but still has to choose what public pile to play on.
    }

    //anything else needed is here.
    //if i need something extra, will add to template as well.
    protected override bool CanEnableDeck()
    {
        return false; //otherwise, can't compile.
    }
    protected override bool CanEnablePile1()
    {
        return VMData.SelfDiscard!.NeedsToDiscardToSelf() == false;
    }
    protected override async Task ProcessDiscardClickedAsync()
    {
        int decks = VMData.PlayerHand1.ObjectSelected();
        if (decks == 0)
        {
            _toast.ShowUserErrorToast("You need to select a card from hand in order to discard to public");
            return;
        }
        await _gameContainer.SendDiscardMessageAsync(decks);
        await _mainGame.DiscardAsync(decks);
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