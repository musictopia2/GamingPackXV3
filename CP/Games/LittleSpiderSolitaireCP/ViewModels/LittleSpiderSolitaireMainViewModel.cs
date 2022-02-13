namespace LittleSpiderSolitaireCP.ViewModels;
[InstanceGame]
[UseLabelGrid]
public partial class LittleSpiderSolitaireMainViewModel : SolitaireMainViewModel<LittleSpiderSolitaireSaveInfo>
{
    public LittleSpiderSolitaireMainViewModel(IEventAggregator aggregator,
        CommandContainer command,
        IGamePackageResolver resolver
        )
        : base(aggregator, command, resolver)
    {
        GlobalClass.MainModel = this;
    }
    protected override SolitaireGameClass<LittleSpiderSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
    {
        return resolver.ReplaceObject<LittleSpiderSolitaireMainGameClass>();
    }
}