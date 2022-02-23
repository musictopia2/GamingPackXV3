
namespace LifeBoardGameBlazor.Views;
public partial class LifeBoardGameMainView
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(LifeBoardGameVMData.NormalTurn))
                .AddLabel("Instructions", nameof(LifeBoardGameVMData.Instructions))
                .AddLabel("Status", nameof(LifeBoardGameVMData.Status));
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;

}