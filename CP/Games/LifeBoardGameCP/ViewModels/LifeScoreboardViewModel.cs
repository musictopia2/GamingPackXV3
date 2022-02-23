namespace LifeBoardGameCP.ViewModels;
[InstanceGame]
public class LifeScoreboardViewModel : ScreenViewModel, IMainScreen
{
    public LifeScoreboardViewModel(IEventAggregator aggregator) : base(aggregator)
    {
    }
}