namespace MancalaBlazor.Views;
public partial class MancalaMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(MancalaVMData.NormalTurn))
            .AddLabel("Status", nameof(MancalaVMData.Status));
        base.OnInitialized();
    }
}