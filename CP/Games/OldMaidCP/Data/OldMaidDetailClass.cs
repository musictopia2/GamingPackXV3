namespace OldMaidCP.Data;
[SingletonGame]
public class OldMaidDetailClass : IGameInfo, ICardInfo<RegularSimpleCard>,
    IBeginningRegularCards<RegularSimpleCard>
{
    EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

    bool IGameInfo.CanHaveExtraComputerPlayers => false;

    EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.ComputerOnly;

    EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

    string IGameInfo.GameName => "Old Maid";

    int IGameInfo.NoPlayers => 0;

    int IGameInfo.MinPlayers => 2;

    int IGameInfo.MaxPlayers => 4;

    bool IGameInfo.CanAutoSave => true;

    EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyTablet;

    EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Landscape;

    int ICardInfo<RegularSimpleCard>.CardsToPassOut => 7;

    BasicList<int> ICardInfo<RegularSimpleCard>.PlayerExcludeList => new();

    bool ICardInfo<RegularSimpleCard>.AddToDiscardAtBeginning => false;

    bool ICardInfo<RegularSimpleCard>.ReshuffleAllCardsFromDiscard => false;

    bool ICardInfo<RegularSimpleCard>.ShowMessageWhenReshuffling => true;

    bool ICardInfo<RegularSimpleCard>.PassOutAll => true;

    bool ICardInfo<RegularSimpleCard>.PlayerGetsCards => true;

    bool ICardInfo<RegularSimpleCard>.NoPass => false;

    bool ICardInfo<RegularSimpleCard>.NeedsDummyHand => false;

    DeckRegularDict<RegularSimpleCard> ICardInfo<RegularSimpleCard>.DummyHand { get; set; } = new DeckRegularDict<RegularSimpleCard>();

    bool ICardInfo<RegularSimpleCard>.HasDrawAnimation => true;

    bool ICardInfo<RegularSimpleCard>.CanSortCardsToBeginWith => true;

    BasicList<int> ICardInfo<RegularSimpleCard>.DiscardExcludeList(IListShuffler<RegularSimpleCard> deckList)
    {
        return new();
    }
    bool IBeginningRegularCards<RegularSimpleCard>.AceLow => true;
    bool IBeginningRegularCards<RegularSimpleCard>.CustomDeck => true;
}