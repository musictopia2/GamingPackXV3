namespace RookBlazor.Views;
public partial class RookBiddingView
{
    private ICustomCommand BidCommand => DataContext!.BidCommand!;
    private ICustomCommand PassCommand => DataContext!.PassCommand!;
}