namespace BasicGameFrameworkLibrary.SolitaireClasses.TriangleClasses;
public class SavedTriangle
{
    public bool InPlay { get; set; }
    public BasicList<SolitaireCard> CardList { get; set; } = new();
}