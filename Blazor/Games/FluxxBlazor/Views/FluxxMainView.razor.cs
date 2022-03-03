namespace FluxxBlazor.Views;
public partial class FluxxMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<ScoreColumnModel> _scores = new();
    private FluxxVMData? _vmData;
    private FluxxGameContainer? _gameContainer;

    protected override void OnInitialized()
    {
        _vmData = aa.Resolver!.Resolve<FluxxVMData>();
        _gameContainer = aa.Resolver.Resolve<FluxxGameContainer>();
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(FluxxVMData.NormalTurn))
            .AddLabel("Status", nameof(FluxxVMData.Status));
        _scores.Clear();
        _scores.AddColumn("Cards Left", true, nameof(FluxxPlayerItem.ObjectCount))

            ; //cards left is common.  can be anything you need.
        base.OnInitialized();
    }
}