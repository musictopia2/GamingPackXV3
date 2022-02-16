namespace MancalaCP.Data;
public class MancalaPlayerItem : SimplePlayer
{ //anything needed is here
    public int HowManyPiecesAtHome { get; set; }
    public BasicList<PlayerPieceData> ObjectList { get; set; } = new();
}