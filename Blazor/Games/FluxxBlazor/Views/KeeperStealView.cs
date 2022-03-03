namespace FluxxBlazor.Views;
public class KeeperStealView : KeeperProcessView<KeeperStealViewModel>
{
    protected override ICustomCommand? Command => DataContext!.ProcessCommand!;
}
