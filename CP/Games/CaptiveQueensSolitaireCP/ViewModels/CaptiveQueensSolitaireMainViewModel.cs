namespace CaptiveQueensSolitaireCP.ViewModels;
[InstanceGame]
[UseLabelGrid]
public partial class CaptiveQueensSolitaireMainViewModel : SolitaireMainViewModel<CaptiveQueensSolitaireSaveInfo>
{
    public CaptiveQueensSolitaireMainViewModel(IEventAggregator aggregator,
        CommandContainer command,
        IGamePackageResolver resolver
        )
        : base(aggregator, command, resolver)
    {
    }
    protected override SolitaireGameClass<CaptiveQueensSolitaireSaveInfo> GetGame(IGamePackageResolver resolver)
    {
        return resolver.ReplaceObject<CaptiveQueensSolitaireMainGameClass>();
    }
    //may require rethinking now.
    public static int FirstNumber //iffy
    {
        get
        {
            return 5;
        }
    }
    public static int SecondNumber
    {
        get
        {
            return 6;
        }
    }
}