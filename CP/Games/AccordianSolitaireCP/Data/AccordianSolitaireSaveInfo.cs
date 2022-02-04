namespace AccordianSolitaireCP.Data;
[SingletonGame]
[SourceGeneratedSerialization]
public class AccordianSolitaireSaveInfo : IMappable
{
    public BasicList<int> DeckList { get; set; } = new();
    public DeckRegularDict<AccordianSolitaireCardInfo> HandList = new();
    public int DeckSelected { get; set; }
    public int NewestOne { get; set; }
    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            if (SetProperty(ref _score, value))
            {
                if (_aggregator != null)
                {
                    PublishScore();
                }
            }
        }
    }
    private void PublishScore()
    {
        if (_aggregator == null)
        {
            throw new CustomBasicException("No event aggrevator was sent to publish score.  Rethink");
        }
        _aggregator.Publish(new ScoreEventModel(Score));
    }
    private IEventAggregator? _aggregator;
    public void LoadMod(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
        PublishScore();
    }
}