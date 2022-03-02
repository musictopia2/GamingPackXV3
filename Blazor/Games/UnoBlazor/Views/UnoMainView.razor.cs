namespace UnoBlazor.Views;
public partial class UnoMainView
{
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    private UnoVMData? _vmData;
    private UnoGameContainer? _gameContainer;
    protected override void OnInitialized()
    {
        _vmData = aa.Resolver!.Resolve<UnoVMData>();
        _gameContainer = aa.Resolver.Resolve<UnoGameContainer>();
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(UnoVMData.NormalTurn))
            .AddLabel("Next", nameof(UnoVMData.NextPlayer))
            .AddLabel("Status", nameof(UnoVMData.Status));
        _scores.Clear();
        _scores.AddColumn("Cards Left", true, nameof(UnoPlayerItem.ObjectCount))
            .AddColumn("Total Points", true, nameof(UnoPlayerItem.TotalPoints))
            .AddColumn("Previous Points", true, nameof(UnoPlayerItem.PreviousPoints));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;
}