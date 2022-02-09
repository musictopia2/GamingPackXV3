namespace PokerCP.Data;
[SingletonGame]
public class PokerSaveInfo : IMappable
{
    public BasicList<int> DeckList { get; set; } = new();
    //anything else needed to save a game will be here.
}