namespace TileRummyBlazor.Views;
public partial class TileRummyMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(TileRummyVMData.NormalTurn))
            .AddLabel("Status", nameof(TileRummyVMData.Status));
        base.OnInitialized();
    }
}