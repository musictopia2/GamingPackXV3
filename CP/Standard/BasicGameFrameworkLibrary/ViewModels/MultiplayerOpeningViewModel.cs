namespace BasicGameFrameworkLibrary.ViewModels;
[UseLabelGrid]
public partial class MultiplayerOpeningViewModel<P> : ScreenViewModel, IBlankGameVM, IOpeningMessenger, IReadyNM, IMultiplayerOpeningViewModel where P : class, IPlayerItem, new()
{
    private readonly IMultiplayerSaveState _state;
    private readonly BasicData _data;
    private readonly IGameNetwork _nets;
    private readonly TestOptions _test;
    private readonly IGameInfo _game;
    private readonly IMessageBox _message;
    private EnumRestoreCategory _singleRestore;
    private EnumRestoreCategory _multiRestore;
    private PlayerCollection<P> _playerList = new();
    private PlayerCollection<P>? _saveList;
    private bool _rejoin;
    public MultiplayerOpeningViewModel(CommandContainer commandContainer,
        IMultiplayerSaveState thisState,
        BasicData data,
        IGameNetwork nets,
        TestOptions test,
        IGameInfo game,
        IEventAggregator aggregator,
        IMessageBox message
        ) : base(aggregator)
    {
        CommandContainer = commandContainer;
        CommandContainer.OpenBusy = true; //this was needed too.
        _state = thisState;
        _data = data;
        _nets = nets;
        _test = test;
        _game = game;
        _message = message;
        CreateCommands(commandContainer);
    }
    public bool HasServer => _nets.HasServer;
    partial void CreateCommands(CommandContainer container);
    protected override async Task ActivateAsync()
    {
        _singleRestore = await _state.SinglePlayerRestoreCategoryAsync();
        _multiRestore = await _state.MultiplayerRestoreCategoryAsync();
        if (_multiRestore != EnumRestoreCategory.NoRestore)
        {
            IRetrieveSavedPlayers<P> rr = Resolver!.Resolve<IRetrieveSavedPlayers<P>>();
            string thisStr = await _state.TempMultiSavedAsync();
            if (thisStr != "")
            {
                //can't do my custom error anymore.  hopefully this will fix problem with mancala
                _saveList = await rr.GetPlayerListAsync(thisStr);
                _saveList.RemoveNonHumanPlayers();
            }
        }
        OpeningStatus = EnumOpeningStatus.None;
        ShowOtherChangesBecauseOfNetworkChange();
        await base.ActivateAsync();
        CommandContainer.OpenBusy = false;
    }
    public CommandContainer CommandContainer { get; set; }
    public EnumOpeningStatus OpeningStatus { get; set; } = EnumOpeningStatus.None;
    private void Reset()
    {
        CommandContainer.OpenBusy = false;
        OpeningStatus = EnumOpeningStatus.None;
        ShowOtherChangesBecauseOfNetworkChange(); //i think here too.
    }
    #region "Command Options"
    public bool CanResumeSinglePlayer
    {
        get
        {
            if (OpeningStatus != EnumOpeningStatus.None)
            {
                return false;
            }
            return _singleRestore != EnumRestoreCategory.NoRestore;
        }
    }
    [Command(EnumCommandCategory.Open)]
    public async Task ResumeSinglePlayerAsync()
    {
        await StartSavedGameAsync();
    }
    public bool CanDisconnectEverybody()
    {
        return ClientsConnected > 0; //if nobody is connected, then no need to do so.
    }
    [Command(EnumCommandCategory.Open)]
    public async Task DisconnectEverybodyAsync()
    {
        _playerList.DisconnectEverybody();
        OpeningStatus = EnumOpeningStatus.HostingWaitingForAtLeastOnePlayer; //has to wait for at least one player again.
        await _nets.DisconnectEverybodyAsync();
        CommandContainer.OpenBusy = false;
    }
    public bool CanRejoinMultiplayerGame()
    {
        if (_saveList is null)
        {
            return false;
        }
        if (OpeningStatus != EnumOpeningStatus.None)
        {
            return false; //too late.
        }
        return _saveList.Count > 0;
    }
    [Command(EnumCommandCategory.Open)]
    public async Task RejoinMultiplayerGameAsync()
    {
        _rejoin = true;
        await HostAsync();
    }
    public bool CanResumeMultiplayerGame
    {
        get
        {
            if (OpeningStatus != EnumOpeningStatus.HostingReadyToStart)
            {
                return false;
            }
            if (_multiRestore == EnumRestoreCategory.NoRestore)
            {
                return false;
            }
            BasicList<P> temporaryList = _playerList.GetTemporaryList();
            if (_saveList == null)
            {
                throw new CustomBasicException("Save list was never created to figure out whether can resume multiplayer game.  Rethink");
            }
            return temporaryList.DoesReconcile(_saveList, Items => Items.NickName);
        }
    }
    [Command(EnumCommandCategory.Open)]
    public async Task ResumeMultiplayerGameAsync()
    {
        _data.MultiPlayer = true;
        await StartSavedGameAsync();
    }
    public bool CanStartComputerSinglePlayerGame(int howMany)
    {
        if (howMany == 0)
        {
            return false;
        }
        if (OpeningStatus != EnumOpeningStatus.None)
        {
            return false;
        }
        if (_singleRestore == EnumRestoreCategory.MustRestore)
        {
            return false;
        }
        return OpenPlayersHelper.CanComputer(_game);
    }

    [Command(EnumCommandCategory.Open)]
    public async Task StartComputerSinglePlayerGameAsync(int howManyComputerPlayers)
    {
        StartSingle();
        bool rets;
        if (_test.PlayCategory == EnumTestPlayCategory.Reverse)
        {
            rets = true;
        }
        else
        {
            rets = false;
        }
        _playerList.LoadPlayers(1, howManyComputerPlayers, rets); //i think
        await StartNewGameAsync();
    }
    public bool CanStartPassAndPlayGame(int howMany)
    {
        if (howMany == 0)
        {
            return false;
        }
        if (OpeningStatus != EnumOpeningStatus.None)
        {
            return false;
        }
        if (_singleRestore == EnumRestoreCategory.MustRestore)
        {
            return false; //because you have to restore.
        }
        return OpenPlayersHelper.CanHuman(_game);
    }
    [Command(EnumCommandCategory.Open)]
    public async Task StartPassAndPlayGameAsync(int howManyHumanPlayers)
    {
        StartSingle();
        _playerList.LoadPlayers(howManyHumanPlayers); //i think
        await StartNewGameAsync();
    }
    public bool CanStart(int howManyExtra)
    {
        if (_multiRestore == EnumRestoreCategory.MustRestore)
        {
            return false; //in this case, you can't no matter what because you must restore.
        }
        int tempCount = _playerList.GetTemporaryCount;
        if (tempCount == 0)
        {
            return false;
        }
        if (howManyExtra > 0 && _game.CanHaveExtraComputerPlayers == false)
        {
            return false;// because can't have extra computer players.
        }
        if (howManyExtra > 0)
        {
            if (howManyExtra + tempCount > _game.MaxPlayers)
            {
                return false;
            }
            if (howManyExtra + tempCount < _game.MinPlayers)
            {
                return false;
            }
            if (howManyExtra + tempCount == _game.NoPlayers)
            {
                return false;
            }
        }
        return OpeningStatus == EnumOpeningStatus.HostingReadyToStart; //i think
    }
    [Command(EnumCommandCategory.Open)]
    public async Task StartAsync(int howManyExtra)
    {
        if (howManyExtra > 0)
        {
            _playerList.LoadPlayers(0, howManyExtra);
        }
        _data.MultiPlayer = true;
        await StartNewGameAsync();
    }
    public bool CanHost => OpeningStatus == EnumOpeningStatus.None;

    [Command(EnumCommandCategory.Open)]
    public async Task HostAsync()
    {
        bool rets = await _nets.InitNetworkMessagesAsync(_data.NickName, false); //because you are hosting.
        if (rets == false)
        {
            await _message.ShowMessageAsync("Failed To Connect To Server");
            Reset();
            return;
        }
    }
    public bool CanConnect => OpeningStatus == EnumOpeningStatus.None;
    [Command(EnumCommandCategory.Open)]
    public async Task ConnectAsync()
    {
        CommandContainer.OpenBusy = true;
        CommandContainer.UpdateAll();
        await Task.Delay(20);
        bool rets = await _nets.InitNetworkMessagesAsync(_data.NickName, true);
        if (rets == false)
        {
            await _message.ShowMessageAsync("Failed To Connect To Server");
            Reset();
            return;
        }
        _data.DoFullScreen?.Invoke();
        await _nets.ConnectToHostAsync(); //this will connect to host.
    }
    public bool CanSolitaire => _game.SinglePlayerChoice == EnumPlayerChoices.Solitaire && OpeningStatus == EnumOpeningStatus.None;
    [Command(EnumCommandCategory.Open)]
    public async Task SolitaireAsync()
    {
        StartSingle(); //i think this too.
        await StartNewGameAsync();
    }
    public bool CanCancelConnection => OpeningStatus == EnumOpeningStatus.HostingWaitingForAtLeastOnePlayer || OpeningStatus == EnumOpeningStatus.WaitingForHost;
    //could have others eventually.
    [Command(EnumCommandCategory.Open)]
    public Task CancelConnectionAsync()
    {
        OpeningStatus = EnumOpeningStatus.None;
        PrivateClose();
        CommandContainer.OpenBusy = false;
        return Task.CompletedTask; 
    }

    private async void PrivateClose()
    {
        await _nets.CloseConnectionAsync();
    }

    #endregion
    private void ShowOtherChangesBecauseOfNetworkChange()
    {
        if (OpeningStatus == EnumOpeningStatus.HostingReadyToStart)
        {
            ExtraOptionsVisible = _game.CanHaveExtraComputerPlayers;
        }
        else
        {
            ExtraOptionsVisible = false;
        }
    }
    #region "Properties"
    public bool ExtraOptionsVisible { get; set; }
    [LabelColumn]
    public int ClientsConnected
    {
        get
        {
            if (_playerList.GetTemporaryCount == 0)
            {
                return 0;
            }
            return _playerList.GetTemporaryCount - 1;
        }
    }
    public bool HostCanStart => OpeningStatus == EnumOpeningStatus.HostingReadyToStart;
    public bool CanShowSingleOptions => OpeningStatus == EnumOpeningStatus.None;
    [LabelColumn]
    public int PreviousNonComputerNetworkedPlayers { get; set; }
    #endregion
    private void StartSingle()
    {
        _data.MultiPlayer = false;
        _playerList = new PlayerCollection<P>();
    }
    private async Task StartNewGameAsync()
    {
        await _state.DeleteGameAsync(); //will delete any autosaved game at this point.
        await Aggregator.PublishAsync(new StartMultiplayerGameEventModel<P>(_playerList));
    }
    private async Task StartSavedGameAsync()
    {
        await Aggregator.PublishAsync(new StartAutoresumeMultiplayerGameEventModel());
    }
    async Task IOpeningMessenger.ConnectedToHostAsync(IGameNetwork network, string hostName)
    {
        await _nets!.SendReadyMessageAsync(network.NickName, hostName);
        await FinishWaitingForHostAsync(network);
    }
    private async Task FinishWaitingForHostAsync(IGameNetwork thisCheck)
    {
        OpeningStatus = EnumOpeningStatus.ConnectedToHost;
        _data.Client = true;
        _data.MultiPlayer = true;
        _data.NickName = thisCheck.NickName;
        thisCheck.IsEnabled = true;
        ShowOtherChangesBecauseOfNetworkChange();
        await Aggregator.PublishAsync(new WaitForHostEventModel());
        CommandContainer.UpdateAll();
    }
    async Task IOpeningMessenger.HostConnectedAsync(IGameNetwork network)
    {
        _data.Client = false;
        if (_rejoin)
        {
            await ResumeMultiplayerGameAsync();
            return;
        }
        network.IsEnabled = true; //not sure
        _playerList = new();
        P thisPlayer = new();
        thisPlayer.NickName = _data.NickName;
        thisPlayer.IsHost = true;
        thisPlayer.Id = 1;
        thisPlayer.InGame = true;
        thisPlayer.PlayerCategory = EnumPlayerCategory.OtherHuman;
        _playerList.AddPlayer(thisPlayer);
        if (_saveList != null)
        {
            PreviousNonComputerNetworkedPlayers = _saveList.Count - 1;
        }
        OpeningStatus = EnumOpeningStatus.HostingWaitingForAtLeastOnePlayer;
        CommandContainer.OpenBusy = false;
    }
    async Task IReadyNM.ProcessReadyAsync(string nickName)
    {
        CommandContainer.OpenBusy = true;
        P thisPlayer = new();
        thisPlayer.NickName = nickName;
        thisPlayer.IsHost = false;
        thisPlayer.Id = _playerList.GetTemporaryCount + 1;
        thisPlayer.InGame = true;
        thisPlayer.PlayerCategory = EnumPlayerCategory.OtherHuman;
        _playerList.AddPlayer(thisPlayer);
        OpeningStatus = EnumOpeningStatus.HostingReadyToStart;
        ShowOtherChangesBecauseOfNetworkChange();
        _nets.IsEnabled = true; //try this.
        CommandContainer.OpenBusy = false;
        await Task.CompletedTask;
    }
    Task IOpeningMessenger.WaitingForHostAsync(IGameNetwork network)
    {
        return ClientChangeStatus(EnumOpeningStatus.WaitingForHost, network);
    }
    private Task ClientChangeStatus(EnumOpeningStatus status, IGameNetwork network)
    {
        ShowOtherChangesBecauseOfNetworkChange();
        _data.Client = true;
        _data.MultiPlayer = true; //we do know its multiplayer for sure.
        _data.NickName = network.NickName;
        network.IsEnabled = true; //because you will receive more information from host when host connects.
        CommandContainer.OpenBusy = false; //the one connecting can choose to cancel.
        OpeningStatus = status;
        CommandContainer.UpdateAll();
        return Task.CompletedTask;
    }
    Task IOpeningMessenger.WaitForGameAsync(IGameNetwork network)
    {
        return FinishWaitingForHostAsync(network);
    }
}