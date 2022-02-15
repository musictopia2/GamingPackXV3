namespace TicTacToeBlazor.Views;
public partial class TicTacToeMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(TicTacToeVMData.NormalTurn))
            .AddLabel("Status", nameof(TicTacToeVMData.Status));
        base.OnInitialized();
    }
}