namespace BasicGamingUIBlazorLibrary.BasicControls.SpecializedFrames.BoneyardPiles;
public partial class BaseBoneyardPileBlazor<D, LI> : IDisposable
     where D : class, ILocationDeck, new()
    where LI : class, IScatterList<D>, new()
{
    private bool _disposedValue;

    //had to do a workaround because was unable to scatter for now.
    [Parameter]
    public ScatteringPiecesObservable<D, LI>? BoneyardPile { get; set; }
    private void ShowChange()
    {
        InvokeAsync(() =>
        {
            StateHasChanged();
        });
    }
    protected override void OnInitialized()
    {
        CommandContainer command = aa.Resolver!.Resolve<CommandContainer>();
        command.AddAction(ShowChange);
        base.OnInitialized();
    }
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    private async Task BoardClickedAsync()
    {
        if (BoneyardPile!.BoardCommand!.CanExecute(null) == false)
        {
            return;
        }
        await BoneyardPile.BoardCommand.ExecuteAsync(null);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                CommandContainer command = aa.Resolver!.Resolve<CommandContainer>();
                command.RemoveAction(ShowChange);
            }
            _disposedValue = true;
        }
    }
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}