//using BasicGameFrameworkLibrary.Attributes;
//using BasicGameFrameworkLibrary.CommandClasses;
//using BasicGameFrameworkLibrary.MultiplayerClasses.EventModels;
//using BasicGameFrameworkLibrary.ViewModelInterfaces;
//using MessengingHelpers;
//using MVVMFramework.ViewModels;
//using System.Threading.Tasks;
//namespace BasicGameFrameworkLibrary.ViewModels
//{
//    public class RestoreViewModel : ScreenViewModel, IRestoreVM, IBlankGameVM
//    {
//        public CommandContainer CommandContainer { get; set; }
//        public RestoreViewModel(CommandContainer command, IEventAggregator aggregator) : base(aggregator)
//        {
//            CommandContainer = command;
//        }
//        [Command(EnumCommandCategory.Old)] //try this one.  even if its not your turn, you can still restore.
//        public Task RestoreAsync()
//        {
//            return Aggregator.PublishAsync(new RestoreEventModel());
//        }
//    }
//}

//still needs multiplayer classes to be done first.