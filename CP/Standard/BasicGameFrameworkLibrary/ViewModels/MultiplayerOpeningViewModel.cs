﻿//using BasicGameFrameworkLibrary.Attributes;
//using BasicGameFrameworkLibrary.BasicGameDataClasses;
//using BasicGameFrameworkLibrary.CommandClasses;
//using BasicGameFrameworkLibrary.MiscProcesses;
//using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
//using BasicGameFrameworkLibrary.MultiplayerClasses.EventModels;
//using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
//using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
//using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
//using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
//using BasicGameFrameworkLibrary.TestUtilities;
//using BasicGameFrameworkLibrary.ViewModelInterfaces;
//using CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
//using CommonBasicLibraries.BasicDataSettingsAndProcesses;
//using CommonBasicLibraries.CollectionClasses;
//using MessengingHelpers;
//using MVVMFramework.ViewModels;
//using System;
//using System.Linq;
//using System.Threading.Tasks; //most of the time, i will be using asyncs.
//using aa = BasicGameFrameworkLibrary.DIContainers.Helpers;
//namespace BasicGameFrameworkLibrary.ViewModels
//{
//    public class MultiplayerOpeningViewModel<P> : ScreenViewModel, IBlankGameVM, IOpeningMessenger, IReadyNM, IMultiplayerOpeningViewModel where P : class, IPlayerItem, new()
//    {
//        private readonly IMultiplayerSaveState _state;
//        private readonly BasicData _data;
//        private readonly INetworkMessages _nets;
//        private readonly TestOptions _test;
//        private readonly IGameInfo _game;
//        private readonly IMessageChecker _checker;
//        private EnumRestoreCategory _singleRestore;
//        private EnumRestoreCategory _multiRestore;
//        private PlayerCollection<P> _playerList = new ();
//        private PlayerCollection<P>? _saveList;
//        public MultiplayerOpeningViewModel(CommandContainer commandContainer,
//            IMultiplayerSaveState thisState,
//            BasicData data,
//            INetworkMessages nets, //iffy.
//            TestOptions test,
//            IGameInfo game,
//            IEventAggregator aggregator,
//            IMessageChecker checker
//            ) : base(aggregator)
//        {
//            CommandContainer = commandContainer;
//            CommandContainer.OpenBusy = true; //this was needed too.
//            _state = thisState;
//            _data = data;
//            _nets = nets;
//            _test = test;
//            _game = game;
//            _checker = checker;
//        }
//        protected override async Task ActivateAsync()
//        {
//            _singleRestore = await _state.SinglePlayerRestoreCategoryAsync();
//            _multiRestore = await _state.MultiplayerRestoreCategoryAsync();
//            if (_multiRestore != EnumRestoreCategory.NoRestore)
//            {
//                IRetrieveSavedPlayers<P> rr = aa.Resolver!.Resolve<IRetrieveSavedPlayers<P>>();
//                string thisStr = await _state.TempMultiSavedAsync();
//                if (thisStr != "")
//                {
//                    //can't do my custom error anymore.  hopefully this will fix problem with mancala
//                    _saveList = await rr.GetPlayerListAsync(thisStr);
//                    _saveList.RemoveNonHumanPlayers(); //try this way to avoid extra generics.
//                    //throw new CustomBasicException("You should have not called temporary multisaved because there was none");
//                }
                
//            }
//            OpeningStatus = EnumOpeningStatus.None;
//            ShowOtherChangesBecauseOfNetworkChange();
//            await base.ActivateAsync();
//            CommandContainer.OpenBusy = false;
//        }
//        public CommandContainer CommandContainer { get; set; }
//        public EnumOpeningStatus OpeningStatus { get; set; } = EnumOpeningStatus.None;
//        private void Reset()
//        {
//            CommandContainer.OpenBusy = false;
//            OpeningStatus = EnumOpeningStatus.None;
//            ShowOtherChangesBecauseOfNetworkChange(); //i think here too.
//        }
//        #region "Command Options"
//        public bool CanResumeSinglePlayer
//        {
//            get
//            {
//                if (OpeningStatus != EnumOpeningStatus.None)
//                {
//                    return false;
//                }
//                return _singleRestore != EnumRestoreCategory.NoRestore;
//            }
//        }
//        [Command(EnumCommandCategory.Open)]
//        public async Task ResumeSinglePlayerAsync()
//        {
//            await StartSavedGameAsync();
//        }
//        public bool CanResumeMultiplayerGame
//        {
//            get
//            {
//                if (OpeningStatus != EnumOpeningStatus.HostingReadyToStart)
//                {
//                    return false;
//                }
//                if (_multiRestore == EnumRestoreCategory.NoRestore)
//                {
//                    return false;
//                }
//                BasicList<P> temporaryList = _playerList.GetTemporaryList();
//                if (_saveList == null)
//                {
//                    throw new CustomBasicException("Save list was never created to figure out whether can resume multiplayer game.  Rethink");
//                }
//                return temporaryList.DoesReconcile(_saveList, Items => Items.NickName);
//            }
//        }
//        [Command(EnumCommandCategory.Open)]
//        public async Task ResumeMultiplayerGameAsync()
//        {
//            _data.MultiPlayer = true;
//            await StartSavedGameAsync();
//        }
//        public bool CanStartComputerSinglePlayerGame(int howMany)
//        {
//            if (howMany == 0)
//            {
//                return false;
//            }
//            if (OpeningStatus != EnumOpeningStatus.None)
//            {
//                return false;
//            }
//            if (_singleRestore == EnumRestoreCategory.MustRestore)
//            {
//                return false;
//            }
//            return OpenPlayersHelper.CanComputer(_game);
//        }

//        [Command(EnumCommandCategory.Open)]
//        public async Task StartComputerSinglePlayerGameAsync(int howManyComputerPlayers)
//        {
//            StartSingle();
//            bool rets;
//            if (_test.PlayCategory == EnumTestPlayCategory.Reverse)
//            {
//                rets = true;
//            }
//            else
//            {
//                rets = false;
//            }
//            _playerList.LoadPlayers(1, howManyComputerPlayers, rets); //i think
//            await StartNewGameAsync();
//        }
//        public bool CanStartPassAndPlayGame(int howMany)
//        {
//            if (howMany == 0)
//            {
//                return false;
//            }
//            if (OpeningStatus != EnumOpeningStatus.None)
//            {
//                return false;
//            }
//            if (_singleRestore == EnumRestoreCategory.MustRestore)
//            {
//                return false; //because you have to restore.
//            }
//            return OpenPlayersHelper.CanHuman(_game);
//        }
//        [Command(EnumCommandCategory.Open)]
//        public async Task StartPassAndPlayGameAsync(int howManyHumanPlayers)
//        {
//            StartSingle();
//            _playerList.LoadPlayers(howManyHumanPlayers); //i think
//            await StartNewGameAsync();
//        }
//        public bool CanStart(int howManyExtra)
//        {
//            if (_multiRestore == EnumRestoreCategory.MustRestore)
//            {
//                return false; //in this case, you can't no matter what because you must restore.
//            }
//            int tempCount = _playerList.GetTemporaryCount;
//            if (tempCount == 0)
//            {
//                return false;
//            }
//            if (howManyExtra > 0 && _game.CanHaveExtraComputerPlayers == false)
//            {
//                return false;// because can't have extra computer players.
//            }
//            if (howManyExtra > 0)
//            {
//                if (howManyExtra + tempCount > _game.MaxPlayers)
//                {
//                    return false;
//                }
//                if (howManyExtra + tempCount < _game.MinPlayers)
//                {
//                    return false;
//                }
//                if (howManyExtra + tempCount == _game.NoPlayers)
//                {
//                    return false;
//                }
//            }
//            return OpeningStatus == EnumOpeningStatus.HostingReadyToStart; //i think
//        }
//        [Command(EnumCommandCategory.Open)]
//        public async Task StartAsync(int howManyExtra)
//        {
//            if (howManyExtra > 0)
//            {
//                _playerList.LoadPlayers(0, howManyExtra);
//            }
//            _data.MultiPlayer = true;
//            await StartNewGameAsync();
//        }
//        public bool CanHost => OpeningStatus == EnumOpeningStatus.None;

//        [Command(EnumCommandCategory.Open)]
//        public async Task HostAsync()
//        {
//            bool rets = await _nets.InitNetworkMessagesAsync(_data.NickName, false); //because you are hosting.
//            if (rets == false)
//            {
//                await UIPlatform.ShowMessageAsync("Failed To Connect To Server");
//                Reset();
//                return;
//            }
//        }
//        public bool CanConnect => OpeningStatus == EnumOpeningStatus.None;
//        [Command(EnumCommandCategory.Open)]
//        public async Task ConnectAsync()
//        {
//            CommandContainer.OpenBusy = true;
//            CommandContainer.UpdateAll();
//            await Task.Delay(20);
//            bool rets = await _nets.InitNetworkMessagesAsync(_data.NickName, true);
//            if (rets == false)
//            {
//                await UIPlatform.ShowMessageAsync("Failed To Connect To Server");
//                Reset();
//                return;
//            }
//            _data.DoFullScreen?.Invoke();
//            await _nets.ConnectToHostAsync(); //this will connect to host.
//        }
//        public bool CanSolitaire => _game.SinglePlayerChoice == EnumPlayerChoices.Solitaire && OpeningStatus == EnumOpeningStatus.None;
//        [Command(EnumCommandCategory.Open)]
//        public async Task SolitaireAsync()
//        {
//            StartSingle(); //i think this too.
//            await StartNewGameAsync();
//        }
//        public bool CanCancelConnection => OpeningStatus == EnumOpeningStatus.HostingWaitingForAtLeastOnePlayer || OpeningStatus == EnumOpeningStatus.WaitingForHost;
//        //could have others eventually.
//        [Command(EnumCommandCategory.Open)]
//        public Task CancelConnectionAsync()
//        {
//            OpeningStatus = EnumOpeningStatus.None;
//            PrivateClose();
//            CommandContainer.OpenBusy = false; //has to be set manually now.
//            //CommandContainer.UpdateAll();
//            return Task.CompletedTask; //try to not wait for this.   so if its slow, hopefully no problem (?)
//            //UIPlatform.ShowInfoToast("Cancelled"); //try this way (?)
//        }

//        private async void PrivateClose()
//        {
//            await _nets.CloseConnectionAsync();
//        }

//        #endregion
//        private void ShowOtherChangesBecauseOfNetworkChange()
//        {
//            if (OpeningStatus == EnumOpeningStatus.HostingReadyToStart)
//            {
//                ExtraOptionsVisible = _game.CanHaveExtraComputerPlayers;
//            }
//            else
//            {
//                ExtraOptionsVisible = false; //in this case, do the old fashioned way.
//            }
//            //hopefully the 3 are no longer needed because its blazor now.

//            //OnPropertyChanged(nameof(HostCanStart));
//            //OnPropertyChanged(nameof(ClientsConnected)); // needs to be readonly so nobody can update.
//            //OnPropertyChanged(nameof(CanShowSingleOptions));
//        }
//        #region "Properties"
//        public bool ExtraOptionsVisible { get; set; }
//        public int ClientsConnected
//        {
//            get
//            {
//                if (_playerList.GetTemporaryCount == 0)
//                {
//                    return 0;// if no players, then its obviously 0 still.
//                }
//                return _playerList.GetTemporaryCount - 1; // i think
//            }
//        }
//        public bool HostCanStart => OpeningStatus == EnumOpeningStatus.HostingReadyToStart;
//        public bool CanShowSingleOptions => OpeningStatus == EnumOpeningStatus.None;
//        public int PreviousNonComputerNetworkedPlayers { get; set; }
//        #endregion
//        private void StartSingle()
//        {
//            _data.MultiPlayer = false;
//            _playerList = new PlayerCollection<P>();
//        }
//        private async Task StartNewGameAsync()
//        {
//            await _state.DeleteGameAsync(); //will delete any autosaved game at this point.
//            await Aggregator.PublishAsync(new StartMultiplayerGameEventModel<P>(_playerList));
//        }
//        private async Task StartSavedGameAsync()
//        { 
//            await Aggregator.PublishAsync(new StartAutoresumeMultiplayerGameEventModel());
//        }
//        async Task IOpeningMessenger.ConnectedToHostAsync(IMessageChecker thisCheck, string hostName)
//        {
//            await _nets!.SendReadyMessageAsync(thisCheck.NickName, hostName);
//            await FinishWaitingForHostAsync(thisCheck);
//        }

//        private async Task FinishWaitingForHostAsync(IMessageChecker thisCheck)
//        {
//            OpeningStatus = EnumOpeningStatus.ConnectedToHost; //hopefully okay.
//            _data.Client = true;
//            _data.MultiPlayer = true; //we do know its multiplayer for sure.
//            _data.NickName = thisCheck.NickName;
//            thisCheck.IsEnabled = true; //because you will receive more information from host.
//            ShowOtherChangesBecauseOfNetworkChange();
//            await Aggregator.PublishAsync(new WaitForHostEventModel()); //if everything works, then on client, will simply wait.
//            CommandContainer.UpdateAll();

//        }

//        //async Task IOpeningMessenger.HostNotFoundAsync(IMessageChecker thisCheck)
//        //{
//        //    await UIPlatform.ShowMessageAsync("Unable to connect to host");
//        //    OpeningStatus = EnumOpeningStatus.None;
//        //    thisCheck.IsEnabled = true; //so can try again later.
//        //    ShowOtherChangesBecauseOfNetworkChange();
//        //    CommandContainer.UpdateAll();
//        //}
//        Task IOpeningMessenger.HostConnectedAsync(IMessageChecker thisCheck)
//        {
//            _data.Client = false;
//            thisCheck.IsEnabled = true; //so it can process the message from client;
//            OpeningStatus = EnumOpeningStatus.HostingWaitingForAtLeastOnePlayer;
//            _playerList = new (); //i think here too.
//            P thisPlayer = new ();
//            thisPlayer.NickName = _data.NickName;
//            thisPlayer.IsHost = true;
//            thisPlayer.Id = 1;
//            thisPlayer.InGame = true; //you have to show you are in game obviously to start with.
//            thisPlayer.PlayerCategory = EnumPlayerCategory.OtherHuman;
//            _playerList.AddPlayer(thisPlayer);
//            if (_saveList != null)
//            {
//                PreviousNonComputerNetworkedPlayers = _saveList.Count - 1; //i think
//            }
//            CommandContainer.OpenBusy = false; //because you may decide to cancel.
//            return Task.CompletedTask;
//        }
//        async Task IReadyNM.ProcessReadyAsync(string nickName)
//        {
//            CommandContainer.OpenBusy = true;
//            P thisPlayer = new();
//            thisPlayer.NickName = nickName;
//            thisPlayer.IsHost = false;
//            thisPlayer.Id = _playerList.GetTemporaryCount + 1;
//            thisPlayer.InGame = true; //you have to be in game obviously.
//            thisPlayer.PlayerCategory = EnumPlayerCategory.OtherHuman; //for now, just set to other.
//            _playerList.AddPlayer(thisPlayer);
//            OpeningStatus = EnumOpeningStatus.HostingReadyToStart;
//            ShowOtherChangesBecauseOfNetworkChange();
//            //hopefully those 2 are no longer needed.

//            //NotifyOfCanExecuteChange(nameof(CanStart));
//            //NotifyOfCanExecuteChange(nameof(CanResumeMultiplayerGame));
//            _checker.IsEnabled = true; //try this.
//            CommandContainer.OpenBusy = false; //the runtime error is here.  blazor component then.
//            await Task.CompletedTask;
//        }

//        Task IOpeningMessenger.WaitingForHostAsync(IMessageChecker thisCheck)
//        {
//            return ClientChangeStatus(EnumOpeningStatus.WaitingForHost, thisCheck);
            
//        }

//        private Task ClientChangeStatus(EnumOpeningStatus status, IMessageChecker thisCheck)
//        {
//            ShowOtherChangesBecauseOfNetworkChange();
//            _data.Client = true;
//            _data.MultiPlayer = true; //we do know its multiplayer for sure.
//            _data.NickName = thisCheck.NickName;
//            thisCheck.IsEnabled = true; //because you will receive more information from host when host connects.
//            CommandContainer.OpenBusy = false; //the one connecting can choose to cancel.
//            OpeningStatus = status;
//            //UIPlatform.ShowInfoToast("Host is not connected.  You can either wait for host or cancel");
//            //attempt to do a toast.
//            CommandContainer.UpdateAll();
//            return Task.CompletedTask;
//        }

//        //if waiting for game, hopefully does not need to know who the host is (?)
//        Task IOpeningMessenger.WaitForGameAsync(IMessageChecker thisCheck)
//        {
//            //can't be on opening screen anymore here either.
//            //even though did not send ready message.
//            return FinishWaitingForHostAsync(thisCheck);
//            //return ClientChangeStatus(EnumOpeningStatus.WaitingForGame, thisCheck);
//        }
//    }
//}