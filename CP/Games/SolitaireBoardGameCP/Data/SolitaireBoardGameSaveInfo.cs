namespace SolitaireBoardGameCP.Data;
[SingletonGame]
//[SourceGeneratedSerialization]
public class SolitaireBoardGameSaveInfo : IMappable
{
    public Vector PreviousPiece { get; set; }
    public SolitaireBoardGameCollection SpaceList = new();
}