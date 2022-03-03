namespace FluxxBlazor.Views;
public class KeeperShowView : KeeperBaseView<KeeperShowViewModel>
{
    protected override EnumKeeperCategory KeeperCategory => EnumKeeperCategory.Show;
    protected override ICustomCommand? Command => DataContext!.CloseKeeperCommand;
}