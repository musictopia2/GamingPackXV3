namespace MillebournesBlazor;
public partial class SafetiesBlazor
{
    [CascadingParameter]
    public MillebournesGameContainer? GameContainer { get; set; }
    [CascadingParameter]
    public MillebournesMainViewModel? DataContext { get; set; }
    [Parameter]
    public TeamCP? Team { get; set; }
    private ICustomCommand SafetyCommand => DataContext!.SafetyClickCommand!;
}