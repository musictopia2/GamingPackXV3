namespace XPuzzleCP.Data;
[SingletonGame]
//[SourceGeneratedSerialization]
public class XPuzzleSaveInfo : IMappable
{
    public Vector PreviousOpen { get; set; }

    public XPuzzleCollection SpaceList = new ();
}
//for now, can't use the new source generation because of the vector.  means if i do autoresume, has to do the old reflection way.