namespace ThreeLetterFunBlazor.Views;
public partial class ThreeLetterFunMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(ThreeLetterFunVMData.NormalTurn))
            .AddLabel("Status", nameof(ThreeLetterFunVMData.Status));
        base.OnInitialized();
    }
}