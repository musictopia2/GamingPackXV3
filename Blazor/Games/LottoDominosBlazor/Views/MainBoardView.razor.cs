namespace LottoDominosBlazor.Views;
public partial class MainBoardView
{
    [CascadingParameter]
    public MainBoardViewModel? DataContext { get; set; }
    private GameBoardCP? Board { get; set; }
    protected override void OnInitialized()
    {
        Board = aa.Resolver!.Resolve<GameBoardCP>(); //best way to handle this.
        base.OnInitialized();
    }
}