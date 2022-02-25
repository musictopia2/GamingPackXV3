namespace DiceBoardGamesMultiplayerBlazor.Views;
public partial class DiceBoardGamesMultiplayerMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(DiceBoardGamesMultiplayerVMData.NormalTurn))
                .AddLabel("Roll", nameof(DiceBoardGamesMultiplayerVMData.RollNumber))
                .AddLabel("Status", nameof(DiceBoardGamesMultiplayerVMData.Status));
        _scores.Clear();
        //use addcolumn for the columns to add.
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;
    private ICustomCommand RollCommand => DataContext!.RollDiceCommand!;
}