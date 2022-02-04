//i think this is the most common things i like to do
namespace PokerCP.Data;
public class CustomDeck : IRegularDeckInfo
{
    int IRegularDeckInfo.HowManyDecks => 1; //most games use one deck.  if there is more than one deck, put here.
    bool IRegularDeckInfo.UseJokers => false;
    int IRegularDeckInfo.GetExtraJokers => 0;
    int IRegularDeckInfo.LowestNumber => 2;
    int IRegularDeckInfo.HighestNumber => 14; //aces will usually be low.
    BasicList<ExcludeRCard> IRegularDeckInfo.ExcludeList => new();
    BasicList<EnumSuitList> IRegularDeckInfo.SuitList => EnumSuitList.CompleteList;
    int IDeckCount.GetDeckCount()
    {
        return 52;
    }
}
