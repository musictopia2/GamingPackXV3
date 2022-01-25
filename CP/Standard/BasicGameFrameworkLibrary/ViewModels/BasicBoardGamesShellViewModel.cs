﻿//using BasicGameFrameworkLibrary.BasicGameDataClasses;
//using BasicGameFrameworkLibrary.CommandClasses;
//using BasicGameFrameworkLibrary.DIContainers;
//using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
//using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
//using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
//using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
//using BasicGameFrameworkLibrary.TestUtilities;
//using BasicGameFrameworkLibrary.ViewModelInterfaces;
//using CommonBasicLibraries.BasicDataSettingsAndProcesses;
//using MessengingHelpers;
//using System.Threading.Tasks; //most of the time, i will be using asyncs.
//namespace BasicGameFrameworkLibrary.ViewModels
//{
//    public abstract class BasicBoardGamesShellViewModel<P> : BasicMultiplayerShellViewModel<P>, IBasicBoardGamesShellViewModel where P : class, IPlayerColors, new()
//    {
//        public BasicBoardGamesShellViewModel(
//            IGamePackageResolver mainContainer,
//            CommandContainer container,
//            IGameInfo gameData,
//            BasicData basicData,
//            IMultiplayerSaveState save,
//            TestOptions test,
//            IEventAggregator aggregator
//            ) : base(mainContainer,
//                container,
//                gameData,
//                basicData,
//                save,
//                test,
//                aggregator
//                )
//        {
//            MiscDelegates.ColorsFinishedAsync = CloseColorsAsync; //hopefully this simple this time.
//        }
//        public IBeginningColorViewModel? ColorScreen { get; set; }
//        protected override async Task GetStartingScreenAsync()
//        {
//            if (ColorScreen != null)
//            {
//                await CloseSpecificChildAsync(ColorScreen);
//            }
//            ColorScreen = MainContainer.Resolve<IBeginningColorViewModel>();
//            await LoadScreenAsync(ColorScreen);
//        }
//        protected virtual bool CanOpenMainAfterColors => true; //so overrided versions can do other things.
//        private async Task CloseColorsAsync()
//        {
//            if (ColorScreen == null)
//            {
//                throw new CustomBasicException("The color screen was not even active.  Rethink");
//            }
//            await CloseSpecificChildAsync(ColorScreen);
//            ColorScreen = null;
//            if (CanOpenMainAfterColors)
//            {
//                await StartNewGameAsync();
//            }
//            IAfterColorProcesses processes = MainContainer.Resolve<IAfterColorProcesses>();
//            await processes.AfterChoosingColorsAsync(); //hopefully this simple.
//        }
//    }
//}