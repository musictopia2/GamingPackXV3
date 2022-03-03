namespace BasicMultiplayerTrickCardGamesBlazor.Views;
public partial class BasicMultiplayerTrickCardGamesMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    private BasicMultiplayerTrickCardGamesVMData? _vmData;
    private BasicMultiplayerTrickCardGamesGameContainer? _gameContainer;

    protected override void OnInitialized()
    {
        _vmData = aa.Resolver!.Resolve<BasicMultiplayerTrickCardGamesVMData>();
        _gameContainer = aa.Resolver.Resolve<BasicMultiplayerTrickCardGamesGameContainer>();
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(BasicMultiplayerTrickCardGamesVMData.NormalTurn))
            .AddLabel("Status", nameof(BasicMultiplayerTrickCardGamesVMData.Status));
        _scores.Clear();
        _scores.AddColumn("Cards Left", true, nameof(BasicMultiplayerTrickCardGamesPlayerItem.ObjectCount))

            ; //cards left is common.  can be anything you need.
        base.OnInitialized();
    }
}