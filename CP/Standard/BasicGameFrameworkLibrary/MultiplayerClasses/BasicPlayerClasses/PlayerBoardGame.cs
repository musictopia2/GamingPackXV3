namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
public abstract class PlayerBoardGame<E> : SimplePlayer, IPlayerBoardGame<E>
    where E : IFastEnumColorSimple
{
    public E? Color { get; set; }
    public abstract bool DidChooseColor { get; }
    public abstract void Clear();
}