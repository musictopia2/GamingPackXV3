namespace GolfCardGameBlazor.Views;
public partial class FirstView : IDisposable
{
    [CascadingParameter]
    public GolfCardGameVMData? VMData { get; set; }
    [CascadingParameter]
    public FirstViewModel? DataContext { get; set; }
    private ICustomCommand ChooseCommand => DataContext?.ChooseFirstCardsCommand!;
    private static string ChooseMethod => nameof(FirstViewModel.ChooseFirstCardsAsync);
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Instructions", nameof(FirstViewModel.Instructions));
        DataContext!.CommandContainer.AddAction(ShowChange);
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
        DataContext!.CommandContainer.RemoveAction(ShowChange);
    }
}
