namespace SinglePlayerCardGamesCP.Data;
[SingletonGame]
public class SinglePlayerCardGamesSaveInfo : IMappable, ISaveInfo
{
    public BasicList<int> DeckList { get; set; } = new();
    //anything else needed to save a game will be here.
}