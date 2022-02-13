namespace RaglanSolitaireCP.Data;
[SingletonGame]
[Cloneable(explicitDeclaration: false)]
public class RaglanSolitaireSaveInfo : SolitaireSavedClass, IMappable
{
    //anything else needed to save a game will be here.
    public DeckRegularDict<SolitaireCard> StockCards { get; set; } = new DeckRegularDict<SolitaireCard>();
}