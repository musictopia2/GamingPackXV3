//using BasicGameFrameworkLibrary.BasicGameDataClasses;
//using BasicGameFrameworkLibrary.CommandClasses;
//using BasicGameFrameworkLibrary.DIContainers;
//using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
//using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
//using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
//using BasicGameFrameworkLibrary.TestUtilities;
//using CommonBasicLibraries.CollectionClasses;
//using MessengingHelpers;
//using System;
//namespace BasicGameFrameworkLibrary.ViewModels
//{
//    public abstract class BasicTrickShellViewModel<P> : BasicMultiplayerShellViewModel<P>
//        where P : class, IPlayerItem, new()
//    {
//        public BasicTrickShellViewModel(IGamePackageResolver mainContainer,
//            CommandContainer container, 
//            IGameInfo gameData, 
//            BasicData basicData, 
//            IMultiplayerSaveState save, 
//            TestOptions test,
//            IEventAggregator aggregator
//            ) : base(mainContainer, container, gameData, basicData, save, test, aggregator)
//        {
//        }
//        protected override BasicList<Type> GetAdditionalObjectsToReset()
//        {
//            return new BasicList<Type>() { typeof(IAdvancedTrickProcesses) }; //hopefully this simple.
//        }
//    }
//}