namespace SkipboBlazor.Views;
public partial class PlayerPilesView : IDisposable
{
    [CascadingParameter]
    public PlayerPilesViewModel? DataContext { get; set; }
    private SkipboPlayerItem? _player;
    protected override void OnParametersSet()
    {
        _player = DataContext!.GameContainer.PlayerList!.GetWhoPlayer();

        base.OnParametersSet();
    }
    private string DiscardTag => $"discard{_player!.NickName}";
    private string StockTag => $"stock{_player!.NickName}";
    protected override void OnInitialized()
    {
        DataContext!.GameContainer.Command.AddAction(ShowChange);
    }
    private void ShowChange()
    {
        InvokeAsync(() =>
        {
            StateHasChanged();
        });
    }
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    void IDisposable.Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    {
        DataContext!.GameContainer.Command.RemoveAction(ShowChange);
    }
}