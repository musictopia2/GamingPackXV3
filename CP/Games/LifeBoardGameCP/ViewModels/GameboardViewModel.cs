namespace LifeBoardGameCP.ViewModels;
[InstanceGame]
public class GameboardViewModel : ScreenViewModel, IMainScreen
{
    public GameboardViewModel(IEventAggregator aggregator) : base(aggregator)
    {
    }
}