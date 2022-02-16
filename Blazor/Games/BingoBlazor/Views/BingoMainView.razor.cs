namespace BingoBlazor.Views;
public partial class BingoMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(BingoVMData.NormalTurn))
            .AddLabel("Status", nameof(BingoVMData.Status));
        base.OnInitialized();
    }
}