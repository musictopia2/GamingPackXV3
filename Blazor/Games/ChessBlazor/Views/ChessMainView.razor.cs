
namespace ChessBlazor.Views;
public partial class ChessMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(ChessVMData.NormalTurn))
                .AddLabel("Instructions", nameof(ChessVMData.Instructions))
                .AddLabel("Status", nameof(ChessVMData.Status));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;

}