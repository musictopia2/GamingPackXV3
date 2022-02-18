namespace RummyDiceBlazor.Views;
public partial class RummyDiceMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(RummyDiceVMData.NormalTurn))
            .AddLabel("Status", nameof(RummyDiceVMData.Status));
        base.OnInitialized();
    }
}