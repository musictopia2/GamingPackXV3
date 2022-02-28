namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
public partial class PlayerRummyHand<D> : PlayerSingleHand<D>, IPlayerRummyHand<D>, IHandle<UpdateCountEventModel>
    where D : IDeckObject, new()
{
    private readonly IEventAggregator _aggregator;
    public PlayerRummyHand()
    {
        _aggregator = Resolver!.Resolve<IEventAggregator>();
        Subscribe(); //hopefully this simple (?)
    }
    public DeckRegularDict<D> AdditionalCards { get; set; } = new DeckRegularDict<D>(); //taking a risk.  hopefully it pays off.
    public override int ObjectCount => MainHandList.Count + _tempCards; //hopefully this simple.
    private int _tempCards;
    public void Handle(UpdateCountEventModel message)
    {
        _tempCards = message.ObjectCount;
    }
    public void DoInit()
    {
        EventAggravatorProcesses.GlobalEventAggravatorClass.ClearSubscriptions(_aggregator);
        //this will subscribe as needed.
        Subscribe();
    }
    private partial void Subscribe();
    private partial void Unsubscribe();
    public void Close()
    {
        Unsubscribe();
    }
}