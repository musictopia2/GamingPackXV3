namespace CousinRummyBlazor.Views;
public partial class CousinRummyMainView
{
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    private CousinRummyVMData? _vmData;
    private CousinRummyGameContainer? _gameContainer;
    private static string GetFirstRows => bb.RepeatAuto(3);
    private static string GetSecondColumns => bb.RepeatAuto(3);
    protected override void OnInitialized()
    {
        _vmData = aa.Resolver!.Resolve<CousinRummyVMData>();
        _gameContainer = aa.Resolver.Resolve<CousinRummyGameContainer>();
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(CousinRummyVMData.NormalTurn))
            .AddLabel("Other Turn", nameof(CousinRummyVMData.OtherLabel))
            .AddLabel("Phase", nameof(CousinRummyVMData.PhaseData));
        _scores.Clear();
        _scores.AddColumn("Cards Left", false, nameof(CousinRummyPlayerItem.ObjectCount))
            .AddColumn("Tokens Left", false, nameof(CousinRummyPlayerItem.TokensLeft))
            .AddColumn("Current Score", false, nameof(CousinRummyPlayerItem.CurrentScore))
            .AddColumn("Total Score", false, nameof(CousinRummyPlayerItem.TotalScore));
        base.OnInitialized();
    }
    private ICustomCommand InitCommand => DataContext!.FirstSetsCommand!;
    private ICustomCommand OtherCommand => DataContext!.OtherSetsCommand!;
    private ICustomCommand BuyCommand => DataContext!.BuyCommand!;
    private ICustomCommand PassCommand => DataContext!.PassCommand!;
}