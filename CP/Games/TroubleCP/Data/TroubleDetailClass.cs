namespace TroubleCP.Data;
[SingletonGame]
public class TroubleDetailClass : IGameInfo
{
    EnumGameType IGameInfo.GameType => EnumGameType.NewGame;

    bool IGameInfo.CanHaveExtraComputerPlayers => false;

    EnumPlayerChoices IGameInfo.SinglePlayerChoice => EnumPlayerChoices.HumanOnly;

    EnumPlayerType IGameInfo.PlayerType => EnumPlayerType.SingleAndNetworked;

    string IGameInfo.GameName => "Trouble";

    int IGameInfo.NoPlayers => 0;

    int IGameInfo.MinPlayers => 2;

    int IGameInfo.MaxPlayers => 4;

    bool IGameInfo.CanAutoSave => true;

    EnumSmallestSuggested IGameInfo.SmallestSuggestedSize => EnumSmallestSuggested.AnyDevice;

    EnumSuggestedOrientation IGameInfo.SuggestedOrientation => EnumSuggestedOrientation.Portrait;
}