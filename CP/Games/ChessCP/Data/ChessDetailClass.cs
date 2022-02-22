namespace ChessCP.Data;
[SingletonGame]
public class ChessDetailClass : IGameInfo
{
    EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

    bool IGameInfo.CanHaveExtraComputerPlayers => false;

    EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.HumanOnly;

    EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

    string IGameInfo.GameName => "Chess";

    int IGameInfo.NoPlayers => 0;

    int IGameInfo.MinPlayers => 2;

    int IGameInfo.MaxPlayers => 2;

    bool IGameInfo.CanAutoSave => true;

    EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice; //default to smallest but can change as needed.

    EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait; //default to portrait but can change to what is needed.
}