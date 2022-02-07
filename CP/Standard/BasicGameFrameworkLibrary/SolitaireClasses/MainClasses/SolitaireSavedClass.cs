
namespace BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
public class SolitaireSavedClass : IMappable
{
    //if you are using the solitairesavedclass, then problems because the saveddiscard will have issues.
    public int Score { get; set; }
    public SavedDiscardPile<SolitaireCard> Discard { get; set; } = new ();
    public string MainPileData { get; set; } = "";
    public SavedWaste WasteData { get; set; } = new SavedWaste();
    public BasicList<int> IntDeckList { get; set; } = new();
}