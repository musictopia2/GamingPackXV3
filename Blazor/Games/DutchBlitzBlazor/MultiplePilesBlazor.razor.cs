namespace DutchBlitzBlazor;
public partial class MultiplePilesBlazor : IDisposable
{
    [CascadingParameter]
    public int TargetHeight { get; set; }
    [Parameter]
    public BasicMultiplePilesCP<DutchBlitzCardInformation>? Piles { get; set; }
    [Parameter]
    public string AnimationTag { get; set; } = "";
    [Parameter]
    public bool Inline { get; set; } = true;
    private string RealHeight => TargetHeight.HeightString();
    private CommandContainer? _command;
    protected override void OnInitialized()
    {
        _command = aa.Resolver!.Resolve<CommandContainer>();
        _command.AddAction(ShowChange);
        base.OnInitialized();
    }
    private void ShowChange()
    {
        InvokeAsync(StateHasChanged);
    }
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    void IDisposable.Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    {
        _command!.RemoveAction(ShowChange);
    }
}