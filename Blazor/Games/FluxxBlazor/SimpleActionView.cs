namespace FluxxBlazor;
public class SimpleActionView : KeyComponentBase, IDisposable
{
    [CascadingParameter]
    public CompleteContainerClass? CompleteContainer { get; set; }
    protected override void OnInitialized()
    {
        CompleteContainer!.GameContainer.Command.AddAction(ShowChange);
        base.OnInitialized();
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
        CompleteContainer!.GameContainer.Command.RemoveAction(ShowChange);
    }
}