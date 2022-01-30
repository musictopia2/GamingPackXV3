namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
public interface IPlayerRummyHand<D> : IPlayerSingleHand<D>, IHandle<UpdateCountEventModel>
    where D : IDeckObject, new()
{
    DeckRegularDict<D> AdditionalCards { get; set; }
}