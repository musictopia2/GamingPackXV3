namespace DemonSolitaireCP.Data;
[SingletonGame]
[Cloneable(explicitDeclaration: false)]
public class DemonSolitaireSaveInfo : SolitaireSavedClass, IMappable
{
    //anything else needed to save a game will be here.
    public BasicList<int> HeelList { get; set; } = new();
}