
namespace CheckersBlazor.Views;
public partial class CheckersMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(CheckersVMData.NormalTurn))
                .AddLabel("Instructions", nameof(CheckersVMData.Instructions))
                .AddLabel("Status", nameof(CheckersVMData.Status));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;

}