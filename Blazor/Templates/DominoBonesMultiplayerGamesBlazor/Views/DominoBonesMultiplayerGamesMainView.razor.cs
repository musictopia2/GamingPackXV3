namespace DominoBonesMultiplayerGamesBlazor.Views;
public partial class DominoBonesMultiplayerGamesMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(DominoBonesMultiplayerGamesVMData.NormalTurn))
            .AddLabel("Status", nameof(DominoBonesMultiplayerGamesVMData.Status));
        base.OnInitialized();
    }
}