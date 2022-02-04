namespace TriangleSolitaireBlazor.Views;
public partial class TriangleSolitaireMainView
{
    protected override void OnInitialized()
    {
        DataContext!.StateHasChanged = () => InvokeAsync(StateHasChanged);
        base.OnInitialized();
    }
}