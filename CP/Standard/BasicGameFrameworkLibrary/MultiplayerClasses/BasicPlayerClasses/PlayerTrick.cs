namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
public class PlayerTrick<S, T> : PlayerSingleHand<T>, IPlayerTrick<S, T>
    where S : IFastEnumSimple
    where T : ITrickCard<S>, new()
{
    public int TricksWon { get; set; }
}