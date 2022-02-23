
namespace BasicMultiplayerDiceGamesBlazor.Views;
public partial class BasicMultiplayerDiceGamesMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(BasicMultiplayerDiceGamesVMData.NormalTurn))
                .AddLabel("Instructions", nameof(BasicMultiplayerDiceGamesVMData.Instructions))
                .AddLabel("Status", nameof(BasicMultiplayerDiceGamesVMData.Status));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;

}