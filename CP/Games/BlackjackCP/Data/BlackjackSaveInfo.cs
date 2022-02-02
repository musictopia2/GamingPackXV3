namespace BlackjackCP.Data;
[SingletonGame]
[SourceGeneratedSerialization]
public class BlackjackSaveInfo : IMappable
{
    public BasicList<int> DeckList { get; set; } = new();
    //anything else needed to save a game will be here.
}