namespace TeeItUpBlazor;
public partial class CardBoardBlazor
{
    [Parameter]
    public GameBoardObservable<TeeItUpCardInformation>? DataContext { get; set; }
}