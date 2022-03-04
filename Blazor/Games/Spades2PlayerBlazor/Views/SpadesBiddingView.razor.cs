namespace Spades2PlayerBlazor.Views;
public partial class SpadesBiddingView
{
    private ICustomCommand BidCommand => DataContext!.BidCommand!;
}