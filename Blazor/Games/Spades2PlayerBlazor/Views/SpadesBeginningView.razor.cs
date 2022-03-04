namespace Spades2PlayerBlazor.Views;
public partial class SpadesBeginningView
{
    private ICustomCommand TakeCommand => DataContext!.TakeCardCommand!;
}