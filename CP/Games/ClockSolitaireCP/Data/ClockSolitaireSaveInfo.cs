namespace ClockSolitaireCP.Data;
[SingletonGame]
[SourceGeneratedSerialization]
public class ClockSolitaireSaveInfo : IMappable
{
    public BasicList<int> DeckList { get; set; } = new();
    public BasicList<ClockInfo> SavedClocks = new();
    public int CurrentCard { get; set; }
    public int PreviousOne { get; set; }
    private int _cardsLeft;
    public int CardsLeft
    {
        get { return _cardsLeft; }
        set
        {
            if (SetProperty(ref _cardsLeft, value))
            {
                Publish();
            }
        }
    }
    private void Publish()
    {
        if (_aggregator == null)
        {
            return;
        }
        _aggregator.Publish(new CardsLeftEventModel(CardsLeft));
    }
    private IEventAggregator? _aggregator;
    public void LoadMod(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        Publish();
    }
}