namespace CountdownBlazor.Views;
public partial class CountdownMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(CountdownVMData.NormalTurn))
                .AddLabel("Roll", nameof(CountdownVMData.RollNumber))
                .AddLabel("Status", nameof(CountdownVMData.Status));
        _scores.Clear();
        //use addcolumn for the columns to add.
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;
    private ICustomCommand RollCommand => DataContext!.RollDiceCommand!;
}