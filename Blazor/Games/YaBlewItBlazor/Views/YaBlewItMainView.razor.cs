namespace YaBlewItBlazor.Views;
public partial class YaBlewItMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    private YaBlewItVMData? _vmData;
    private YaBlewItGameContainer? _gameContainer;

    protected override void OnInitialized()
    {
        _vmData = aa.Resolver!.Resolve<YaBlewItVMData>();
        _gameContainer = aa.Resolver.Resolve<YaBlewItGameContainer>();
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(YaBlewItVMData.NormalTurn))
            .AddLabel("Status", nameof(YaBlewItVMData.Status));
        _scores.Clear();
        _scores.AddColumn("Cards Left", true, nameof(YaBlewItPlayerItem.ObjectCount))

            ; //cards left is common.  can be anything you need.
        base.OnInitialized();
    }
}