namespace LottoDominosBlazor.Views;
public partial class LottoDominosMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(LottoDominosVMData.NormalTurn))
            .AddLabel("Status", nameof(LottoDominosVMData.Status));
        base.OnInitialized();
    }
}