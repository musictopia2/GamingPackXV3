//using BasicGameFrameworkLibrary.Attributes;
//using BasicGameFrameworkLibrary.BasicEventModels;
//using BasicGameFrameworkLibrary.BasicGameDataClasses;
//using BasicGameFrameworkLibrary.CommandClasses;
//using BasicGameFrameworkLibrary.ViewModelInterfaces;
//using MessengingHelpers;
//using MVVMFramework.ViewModels;
//using System.Threading.Tasks;
//namespace BasicGameFrameworkLibrary.ViewModels
//{
//    public class NewRoundViewModel : ScreenViewModel, INewRoundVM, IBlankGameVM
//    {
//        private readonly BasicData _basicData;
//        public CommandContainer CommandContainer { get; set; }
//        public NewRoundViewModel(CommandContainer command, IEventAggregator aggregator, BasicData basicData) : base(aggregator)
//        {
//            CommandContainer = command;
//            _basicData = basicData;
//        }
//        public bool CanStartNewRound
//        {
//            get
//            {
//                if (_basicData.MultiPlayer == false)
//                {
//                    return true;
//                }
//                return _basicData.Client == false;
//            }
//        }
//        [Command(EnumCommandCategory.Old)]
//        public Task StartNewRoundAsync()
//        {
//            return Aggregator.PublishAsync(new NewRoundEventModel()); //this does not care what happens with the new round.
//        }
//    }
//}

//once i put in the source generator, then can do this most likely.