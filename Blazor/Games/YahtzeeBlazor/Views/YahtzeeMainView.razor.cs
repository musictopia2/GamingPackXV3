namespace YahtzeeBlazor.Views;
public partial class YahtzeeMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(YahtzeeVMData.NormalTurn))
                .AddLabel("Roll", nameof(YahtzeeVMData.RollNumber))
                .AddLabel("Status", nameof(YahtzeeVMData.Status));
        _scores.Clear();
        //use addcolumn for the columns to add.
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;
    private ICustomCommand RollCommand => DataContext!.RollDiceCommand!;
}