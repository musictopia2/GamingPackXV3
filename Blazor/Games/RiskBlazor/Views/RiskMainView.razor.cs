
namespace RiskBlazor.Views;
public partial class RiskMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(RiskVMData.NormalTurn))
                .AddLabel("Instructions", nameof(RiskVMData.Instructions))
                .AddLabel("Status", nameof(RiskVMData.Status));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;

}