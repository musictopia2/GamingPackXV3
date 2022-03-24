namespace SavannahCP.Logic;
[SingletonGame]
public class SavannahMainGameClass
    : CardGameClass<RegularSimpleCard, SavannahPlayerItem, SavannahSaveInfo>
    , IMiscDataNM, ISerializable
{
    private readonly SavannahVMData _model;
    private readonly CommandContainer _command; //most of the time, needs this.  if not needed, take out.
    public StandardRollProcesses<SimpleDice, SavannahPlayerItem> Roller;
    private readonly SavannahGameContainer _gameContainer; //if we don't need it, take it out.
    private bool _wasNew;
    private bool _willClearBoard;
    public SavannahMainGameClass(IGamePackageResolver mainContainer,
        IEventAggregator aggregator,
        BasicData basicData,
        TestOptions test,
        SavannahVMData currentMod,
        IMultiplayerSaveState state,
        IAsyncDelayer delay,
        ICardInfo<RegularSimpleCard> cardInfo,
        CommandContainer command,
        SavannahGameContainer gameContainer,
        StandardRollProcesses<SimpleDice, SavannahPlayerItem> roller,
        ISystemError error,
        IToast toast
        ) : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, cardInfo, command, gameContainer, error, toast)
    {
        _model = currentMod;
        _command = command;
        Roller = roller;
        Roller.AfterRollingAsync = AfterRollingAsync;
        Roller.CurrentPlayer = () => SingleInfo!;
        _gameContainer = gameContainer;
    }
    public override Task FinishGetSavedAsync()
    {
        LoadControls();
        _model!.LoadCup(SaveRoot, true);
        _model.SelfDiscard = new(_command, _gameContainer);
        _model.SelfDiscard.Reload(); //try this too (?)
        _model.PublicPiles!.PileList!.ReplaceRange(SaveRoot.PublicPileList);
        var player = PlayerList.GetSelf();
        LoadPlayerStockPiles(player);
        //anything else needed is here.
        return base.FinishGetSavedAsync();
    }
    private void LoadPlayerStockPiles(SavannahPlayerItem player)
    {
        //has to add cards to it now.
        _model.SelfStock.ClearCards(); //clear out because its being reloaded.
        player.ReserveList.ForEach(card =>
        {
            _model.SelfStock.AddCard(card);
        });
        //later has to think about how to handle the removing cards (when you get rid of them).
    }
    public override Task PopulateSaveRootAsync()
    {
        SaveRoot!.PublicPileList = _model.PublicPiles.PileList!.ToBasicList();
        //may have to think about the stock stuff (?)
        return base.PopulateSaveRootAsync();
    }
    private void LoadControls()
    {
        if (IsLoaded == true)
        {
            return;
        }

        IsLoaded = true; //i think needs to be here.
    }
    protected override async Task ComputerTurnAsync()
    {
        //if there is nothing, then just won't do anything.
        await Task.CompletedTask;
    }
    protected override Task StartSetUpAsync(bool isBeginning)
    {
        if (IsLoaded == false)
        {
            _model!.LoadCup(SaveRoot, false);
        }
        LoadControls();
        SaveRoot!.ImmediatelyStartTurn = true;
        return base.StartSetUpAsync(isBeginning);
    }
    private int CardsLeftForDiscard()
    {
        int count = 3;
        foreach (var player in PlayerList)
        {
            count += player.MainHandList.Count;
            count += player.DiscardList.Count;
            count += player.ReserveList.Count;
        }
        int output = _gameContainer.DeckCount - count;
        return output;
    }
    public bool EnoughCards()
    {
        int lefts= CardsLeftForDiscard();
        if (lefts == 0)
        {
            return false;
        }
        if (lefts >= 4)
        {
            return true;
        }
        return false; //because we have more than 2 players now.
    }
    Task IMiscDataNM.MiscDataReceived(string status, string content)
    {
        switch (status) //can't do switch because we don't know what the cases are ahead of time.
        {
            //put in cases here.

            default:
                throw new CustomBasicException($"Nothing for status {status}  with the message of {content}");
        }
    }
    public override async Task StartNewTurnAsync()
    {
        await base.StartNewTurnAsync();
        if (SingleInfo!.PlayerCategory == EnumPlayerCategory.Computer)
        {
            await EndTurnAsync(); //has to end turn here instead of continueturn so it will not even roll for the computer player.
            return;
        }
        if (SingleInfo.PlayerCategory == EnumPlayerCategory.OtherHuman)
        {
            Network!.IsEnabled = true; //waiting.  has to come from other players for rolling dice.
            return;
        }
        SaveRoot.ChoseOtherPlayer = false;
        _model.Cup!.ClearDice(); //i think.
        await Roller!.RollDiceAsync(); //does automatically here
    }
    protected override Task LastPartOfSetUpBeforeBindingsAsync()
    {
        PlayerList.ForEach(player =>
        {
            var list = player.MainHandList.ToBasicList();
            player.WhenToStackDiscards = 5; //0 based.
            player.MainHandList.Clear();
            player.ReserveList.ReplaceRange(list.Take(13));
            player.DiscardList.ReplaceRange(list.Skip(13).Take(6));
            if (player.PlayerCategory == EnumPlayerCategory.Self)
            {
                LoadPlayerStockPiles(player);
            }
        });
        _model.SelfDiscard = new(_command, _gameContainer);
        _model.SelfDiscard.ClearBoard();
        var fins = _model.Deck1.DrawSeveralCards(3);
        _model.PublicPiles.ClearBoard(fins);
        return base.LastPartOfSetUpBeforeBindingsAsync();
    }
    public override async Task EndTurnAsync()
    {
        SingleInfo = PlayerList!.GetWhoPlayer();
        SingleInfo.MainHandList.UnhighlightObjects(); //i think this is best.

        //anything else is here.  varies by game.


        _command.ManuelFinish = true; //because it could be somebody else's turn.
        WhoTurn = await PlayerList.CalculateWhoTurnAsync();
        await StartNewTurnAsync();
    }
    public async Task AfterRollingAsync()
    {
        await StartDrawingAsync();
    }
    private async Task StartDrawingAsync()
    {
        int needs = 4 - SingleInfo!.MainHandList.Count;
        int lefts = CardsLeftForDiscard();
        if (lefts == 0)
        {
            throw new CustomBasicException("Cannot be 0 cards left.  Find out what happened");
        }
        if (lefts < needs)
        {
            LeftToDraw = lefts; //there is somehow not enough cards no matter what.
        }
        else
        {
            LeftToDraw = needs;
        }
        _wasNew = SingleInfo.MainHandList.Count == 0;
        PlayerDraws = WhoTurn;
        _willClearBoard = false;
        await DrawAsync();
    }
    protected override Task AfterDrawingAsync()
    {
        if (_wasNew == true)
        {
            SingleInfo!.MainHandList.UnhighlightObjects();
            SortCards(); //i think.
        }

        return base.AfterDrawingAsync();
    }
}