namespace CandylandBlazor.Views;
public partial class CandylandMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(CandylandVMData.NormalTurn))
            .AddLabel("Status", nameof(CandylandVMData.Status));
        base.OnInitialized();
    }
}