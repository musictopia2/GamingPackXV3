namespace YaBlewItCP.Data;
[SingletonGame]
public class YaBlewItDetailClass : IGameInfo, ICardInfo<YaBlewItCardInformation>
{
    EnumGameType IGameInfo.GameType => EnumGameType.NewGame;
    bool IGameInfo.CanHaveExtraComputerPlayers => false;
    EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.HumanOnly;
    EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.NetworkOnly;
    string IGameInfo.GameName => "Game";
    int IGameInfo.NoPlayers => 0;
    int IGameInfo.MinPlayers => 2;
    int IGameInfo.MaxPlayers => 4;
    bool IGameInfo.CanAutoSave => true;
    EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice; //default to smallest but can change as needed.
    EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape; //default to portrait but can change to what is needed.
    int ICardInfo<YaBlewItCardInformation>.CardsToPassOut => 7; //change to what you need.
    BasicList<int> ICardInfo<YaBlewItCardInformation>.PlayerExcludeList => new();
    BasicList<int> ICardInfo<YaBlewItCardInformation>.DiscardExcludeList(IListShuffler<YaBlewItCardInformation> deckList)
    {
        return new();
    }
    bool ICardInfo<YaBlewItCardInformation>.AddToDiscardAtBeginning => true;
    bool ICardInfo<YaBlewItCardInformation>.ReshuffleAllCardsFromDiscard => false;
    bool ICardInfo<YaBlewItCardInformation>.ShowMessageWhenReshuffling => true;
    bool ICardInfo<YaBlewItCardInformation>.PassOutAll => false;
    bool ICardInfo<YaBlewItCardInformation>.PlayerGetsCards => true;
    bool ICardInfo<YaBlewItCardInformation>.NoPass => false;
    bool ICardInfo<YaBlewItCardInformation>.NeedsDummyHand => false;
    DeckRegularDict<YaBlewItCardInformation> ICardInfo<YaBlewItCardInformation>.DummyHand { get; set; } = new DeckRegularDict<YaBlewItCardInformation>();
    bool ICardInfo<YaBlewItCardInformation>.HasDrawAnimation => true;
    bool ICardInfo<YaBlewItCardInformation>.CanSortCardsToBeginWith => true;
}