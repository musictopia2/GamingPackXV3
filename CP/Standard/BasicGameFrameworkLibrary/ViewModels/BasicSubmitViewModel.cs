//using BasicGameFrameworkLibrary.Attributes;
//using BasicGameFrameworkLibrary.CommandClasses;
//using BasicGameFrameworkLibrary.CommonInterfaces;
//using BasicGameFrameworkLibrary.ViewModelInterfaces;
//using MessengingHelpers;
//using MVVMFramework.ViewModels;
//using System.Threading.Tasks;
//namespace BasicGameFrameworkLibrary.ViewModels
//{
//    [InstanceGame]
//    public abstract class BasicSubmitViewModel : ScreenViewModel, IBlankGameVM, IMainScreen, ISubmitText
//    {
//        public abstract bool CanSubmit { get; } //i think this is the best way to go.

//        [Command(EnumCommandCategory.Plain)]
//        public abstract Task SubmitAsync();
//        public virtual string Text => "Submit"; //since this is default, will use this to start with.
//        public BasicSubmitViewModel(CommandContainer commandContainer, IEventAggregator aggregator): base(aggregator)
//        {
//            CommandContainer = commandContainer;
//        }
//        public CommandContainer CommandContainer { get; set; }
//    }
//}

//has to wait until figure out the new attributes.