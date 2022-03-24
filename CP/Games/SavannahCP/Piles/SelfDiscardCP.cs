namespace SavannahCP.Piles;
public class SelfDiscardCP : HandObservable<RegularSimpleCard>
{
    private readonly SavannahPlayerItem _player;
    private readonly SavannahGameContainer _gameContainer;
    public SelfDiscardCP(CommandContainer command, SavannahGameContainer gameContainer) : base(command)
    {
        AutoSelect = EnumHandAutoType.SelectOneOnly;
        _gameContainer = gameContainer;
        _player = _gameContainer.PlayerList!.GetSelf(); //will always be self.
        HasFrame = true;
        Text = "Discard Piles";
    }
    //this is only for self.
    public void ClearBoard()
    {
        if (_player.DiscardList.Count != 6)
        {
            throw new CustomBasicException("There must be only 6 cards for overlapping");
        }
        HandList = _player.DiscardList; //try this way.
        bool unknowns = true;
        foreach (var card in HandList)
        {
            card.IsUnknown = unknowns;
            unknowns = !unknowns;
        }
        if (HandList.Last().IsUnknown)
        {
            throw new CustomBasicException("The last card cannot be unknown");
        }
        Maximum = 6;
    }
    public void RemoveCard()
    {
        //has to remove last card.
        HandList.RemoveLastItem();
        if (HandList.Count + 1 < _player.WhenToStackDiscards)
        {
            _player.WhenToStackDiscards--; //i think.  hopefully this will work.  this means that for now one starts stacking sooner.
            Maximum--;
        }
    }
    public void Reload()
    {
        HandList = _player.DiscardList;
        Maximum = _player.WhenToStackDiscards + 1;
    }
    protected override bool CanSelectSingleObject(RegularSimpleCard thisObject)
    {
        return thisObject.Deck == HandList.Last().Deck; //you can only select the last card.
    }
    private bool RequiresStacking => HandList.Count - 1 > _player.WhenToStackDiscards;
    protected override async Task ProcessSelectOneOnlyAsync(RegularSimpleCard payLoad)
    {
        if (payLoad.IsSelected)
        {
            payLoad.IsSelected = false;
            return;
        }
        if (RequiresStacking)
        {
            await SelectOnlySingleCardAsync(payLoad);
            
            return;
        }
        if (_player.MainHandList.HasSelectedObject())
        {
            if (_gameContainer.DiscardAsync is null)
            {
                throw new CustomBasicException("Nobody is handling the DiscardAsync");
            }
            await _gameContainer.DiscardAsync.Invoke();
            return;
        }
        await SelectOnlySingleCardAsync(payLoad);
    }
    private async Task SelectOnlySingleCardAsync(RegularSimpleCard card)
    {
        if (_gameContainer.UnselectAllPilesAsync is null)
        {
            throw new CustomBasicException("Nobody is handling the unselecting all piles");
        }
        await _gameContainer.UnselectAllPilesAsync.Invoke();
        card.IsSelected = true;
    }
}