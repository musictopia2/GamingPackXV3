namespace FiveCrownsCP.Data;
public class SavedSet
{
    public DeckRegularDict<FiveCrownsCardInformation> CardList { get; set; } = new();
}