namespace BattleshipBlazor.Views;
public partial class BattleshipMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(BattleshipVMData.NormalTurn))
            .AddLabel("Status", nameof(BattleshipVMData.Status));
        base.OnInitialized();
    }
}