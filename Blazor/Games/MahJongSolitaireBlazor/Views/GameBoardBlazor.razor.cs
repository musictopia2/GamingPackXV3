namespace MahJongSolitaireBlazor.Views;
internal struct TileGame
{
    public int Deck { get; set; }
    public int GameNumber { get; set; }
}
public partial class GameBoardBlazor
{
    [Parameter]
    public BasicList<BoardInfo> BoardList { get; set; } = new();
    private static TileGame GetTileKey(MahjongSolitaireTileInfo tile)
    {
        return new TileGame()
        {
            Deck = tile.Deck,
            GameNumber = MahJongSolitaireMainViewModel.GameDrawing
        };
    }
    private static string GetGameKey => $"MahjongGame{MahJongSolitaireMainViewModel.GameDrawing}";
}