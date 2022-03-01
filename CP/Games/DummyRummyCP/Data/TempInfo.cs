namespace DummyRummyCP.Data;
public struct TempInfo
{
    public DeckRegularDict<RegularRummyCard> CardList;
    public int SetNumber { get; set; }
}