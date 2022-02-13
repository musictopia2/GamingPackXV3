namespace EagleWingsSolitaireCP.Data;
[SingletonGame]
[Cloneable(explicitDeclaration: false)]
public class EagleWingsSolitaireSaveInfo : SolitaireSavedClass, IMappable
{
    //anything else needed to save a game will be here.
    public BasicList<int> HeelList { get; set; } = new();
}