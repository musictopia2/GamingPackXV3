
namespace FreeCellSolitaireBlazor.Views;
public partial class FreeCellSolitaireMainView
{
    private readonly BasicList<LabelGridModel> _labels = new();
    protected override void OnInitialized()
    {
        _labels.Clear();
        _labels.AddLabel("Score", nameof(FreeCellSolitaireMainViewModel.Score)); //if there are others, do here.
        base.OnInitialized();
    }
    //private string AutoMoveName => nameof(FreeCellSolitaireMainViewModel.AutoMoveAsync);
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