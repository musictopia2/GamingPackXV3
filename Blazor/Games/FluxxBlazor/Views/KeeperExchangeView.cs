namespace FluxxBlazor.Views;
public class KeeperExchangeView : KeeperProcessView<KeeperExchangeViewModel>
{
    protected override ICustomCommand? Command => DataContext!.ProcessCommand!;
}