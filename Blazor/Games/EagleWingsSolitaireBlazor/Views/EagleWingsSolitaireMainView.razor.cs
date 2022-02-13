
namespace EagleWingsSolitaireBlazor.Views;
public partial class EagleWingsSolitaireMainView
{
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Score", nameof(EagleWingsSolitaireMainViewModel.Score))
            .AddLabel("Starting Number", nameof(EagleWingsSolitaireMainViewModel.StartingNumber));
        base.OnInitialized();
    }
    //private string AutoMoveName => nameof(EagleWingsSolitaireMainViewModel.AutoMoveAsync);
    private BasicMultiplePilesCP<SolitaireCard> GetMainPiles()
    {
        MainPilesCP main = (MainPilesCP)DataContext!.MainPiles1;
        var output = main.Piles;
        return output;
    }
    private SolitairePilesCP GetWastePiles()
    {
        WastePilesCP waste = (WastePilesCP)DataContext!.WastePiles1;
        var output = waste.Piles;
        return output;
        //return (WastePilesCP)DataContext!.WastePiles1;
    }

}