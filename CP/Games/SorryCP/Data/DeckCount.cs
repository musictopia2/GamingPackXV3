namespace SorryCP.Data;
public class DeckCount : IDeckCount
{
    int IDeckCount.GetDeckCount()
    {
        return 45;
    }
}