namespace BasicGamingUIBlazorLibrary.Views;
public partial class YahtzeeMainView<D>
    where D : SimpleDice, new()
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    //private IEventAggregator? _aggregator;
    private int _bottomDescriptionWidth;
    private ScoreContainer? _scoreContainer;
    protected override void OnInitialized()
    {
        //not sure about subscribing for this one.  don't see any interfaces it implemented (?)
        _scoreContainer = null;
        //_aggregator = aa.Resolver!.Resolve<IEventAggregator>();
        //_aggregator.Subscribe(this);
        _labels.Clear();
        IYahtzeeStyle yahtzeeStyle = aa.Resolver!.Resolve<IYahtzeeStyle>();
        _bottomDescriptionWidth = yahtzeeStyle.BottomDescriptionWidth;
        DataContext!.CommandContainer.AddAction(ShowChange);
        //roll number is iffy.
        _labels.AddLabel("Turn", nameof(YahtzeeVMData<D>.NormalTurn))
            .AddLabel("Roll", nameof(YahtzeeVMData<D>.RollNumber))
            .AddLabel("Status", nameof(YahtzeeVMData<D>.Status))
            .AddLabel("Turn #", nameof(YahtzeeVMData<D>.Round));
        _scores.Clear();
        _scores.AddColumn("Points", false, nameof(YahtzeePlayerItem<D>.Points));
        base.OnInitialized();
    }
    private ICustomCommand RollCommand => DataContext!.RollDiceCommand!;
    private static ScoreContainer GetContainer()
    {
        return aa.Resolver!.Resolve<ScoreContainer>();
    }
}