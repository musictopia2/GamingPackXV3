namespace PersianSolitaireCP.Data;
public class CustomDeck : IRegularDeckInfo
{
    int IRegularDeckInfo.HowManyDecks => 2; //most games use one deck.  if there is more than one deck, put here.

    bool IRegularDeckInfo.UseJokers => false;

    int IRegularDeckInfo.GetExtraJokers => 0;

    int IRegularDeckInfo.LowestNumber => 7;

    int IRegularDeckInfo.HighestNumber => 14; //still low but needs to show 14 so it would work properly.

    BasicList<ExcludeRCard> IRegularDeckInfo.ExcludeList => new();

    BasicList<EnumSuitList> IRegularDeckInfo.SuitList => EnumSuitList.CompleteList;

    int IDeckCount.GetDeckCount()
    {
        return 64;
    }
}
