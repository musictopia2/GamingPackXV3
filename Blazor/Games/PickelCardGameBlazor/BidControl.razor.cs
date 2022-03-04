namespace PickelCardGameBlazor;
public partial class BidControl
{
    private ICustomCommand BidCommand => DataContext!.ProcessBidCommand!;
    private ICustomCommand PassCommand => DataContext!.PassCommand!;
    private static string BidMethod => nameof(PickelBidViewModel.ProcessBidAsync);
    private static string PassMethod => nameof(PickelBidViewModel.PassAsync);
}