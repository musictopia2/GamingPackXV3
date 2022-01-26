namespace BasicGamingUIBlazorLibrary.Views;
public partial class NewGameView
{
    [CascadingParameter]
    public NewGameViewModel? DataContext { get; set; }
}