namespace PickelCardGameBlazor;
public partial class BidControl
{
    private ICustomCommand BidCommand => DataContext!.ProcessBidCommand!;
    private ICustomCommand PassCommand => DataContext!.PassCommand!;
}