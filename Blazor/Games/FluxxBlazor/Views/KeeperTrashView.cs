namespace FluxxBlazor.Views;
public class KeeperTrashView : KeeperProcessView<KeeperTrashViewModel>
{
    protected override ICustomCommand? Command => DataContext!.ProcessCommand!;
}