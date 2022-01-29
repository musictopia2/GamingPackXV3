namespace XPuzzleCP.Data;
[SingletonGame]
[SourceGeneratedSerialization]
public class XPuzzleSaveInfo : IMappable
{
    public Vector PreviousOpen { get; set; }

    public XPuzzleCollection SpaceList = new ();
}