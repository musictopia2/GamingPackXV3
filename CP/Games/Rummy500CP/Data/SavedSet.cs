namespace Rummy500CP.Data;
public class SavedSet
{
    public DeckRegularDict<RegularRummyCard> CardList { get; set; } = new();
    public EnumWhatSets SetType;
    public bool UseSecond;
}