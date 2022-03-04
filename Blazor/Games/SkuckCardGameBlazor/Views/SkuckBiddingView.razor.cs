namespace SkuckCardGameBlazor.Views;
public partial class SkuckBiddingView
{
    private ICustomCommand BidCommand => DataContext!.BidCommand!;
}