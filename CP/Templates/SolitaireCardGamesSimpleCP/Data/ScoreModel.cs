namespace SolitaireCardGamesSimpleCP.Data;
[SingletonGame]
public class ScoreModel : IScoreData
{
    private int _score;
    private readonly IEventAggregator _aggregator;
    public int Score
    {
        get { return _score; }
        set
        {
            if (SetProperty(ref _score, value))
            {
                //can decide what to do when property changes
                _aggregator.Publish(this);
            }

        }
    }
    public ScoreModel(IEventAggregator aggregator)
    {
        _aggregator = aggregator;
    }
}