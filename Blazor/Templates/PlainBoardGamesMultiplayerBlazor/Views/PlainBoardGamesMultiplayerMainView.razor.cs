namespace PlainBoardGamesMultiplayerBlazor.Views;
public partial class PlainBoardGamesMultiplayerMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(PlainBoardGamesMultiplayerVMData.NormalTurn))
            .AddLabel("Status", nameof(PlainBoardGamesMultiplayerVMData.Status));
        base.OnInitialized();
    }
}