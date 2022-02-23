namespace LifeBoardGameCP.ViewModels;
[InstanceGame]
public class ShowCardViewModel : ScreenViewModel, IMainScreen
{
    public ShowCardViewModel(IEventAggregator aggregator) : base(aggregator)
    {
    }
}