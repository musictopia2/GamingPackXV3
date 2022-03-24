namespace SavannahCP.Logic;
public class CustomDeck : IRegularDeckInfo
{
    private readonly SavannahGameContainer _gameContainer;

    public CustomDeck(SavannahGameContainer gameContainer)
    {
        _gameContainer = gameContainer;
    }
    int IRegularDeckInfo.HowManyDecks => _gameContainer.PlayerList!.Count;

    bool IRegularDeckInfo.UseJokers => false;

    int IRegularDeckInfo.GetExtraJokers => 0;

    int IRegularDeckInfo.LowestNumber => 1;

    int IRegularDeckInfo.HighestNumber => 13;

    BasicList<ExcludeRCard> IRegularDeckInfo.ExcludeList => new();

    BasicList<EnumSuitList> IRegularDeckInfo.SuitList => EnumSuitList.CompleteList;
    int IDeckCount.GetDeckCount()
    {
        return _gameContainer.DeckCount;
    }
}