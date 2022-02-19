namespace DominosMexicanTrainCP.Data;
public class TrainInfo
{
    public DeckRegularDict<MexicanDomino> DominoList = new ();
    public int Index { get; set; }
    public bool TrainUp { get; set; }
    public bool IsPublic { get; set; }
}