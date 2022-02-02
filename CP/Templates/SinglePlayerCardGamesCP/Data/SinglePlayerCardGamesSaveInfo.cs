namespace SinglePlayerCardGamesCP.Data;
[SingletonGame]
[SourceGeneratedSerialization]
public class SinglePlayerCardGamesSaveInfo : IMappable
{
    public BasicList<int> DeckList { get; set; } = new();
    //anything else needed to save a game will be here.
}