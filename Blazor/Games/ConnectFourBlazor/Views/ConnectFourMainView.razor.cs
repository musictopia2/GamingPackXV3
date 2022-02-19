
namespace ConnectFourBlazor.Views;
public partial class ConnectFourMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(ConnectFourVMData.NormalTurn))
                .AddLabel("Instructions", nameof(ConnectFourVMData.Instructions))
                .AddLabel("Status", nameof(ConnectFourVMData.Status));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;

}