namespace HuseHeartsCP.ViewModels;
public class PassingViewModel : BasicSubmitViewModel
{
    private readonly HuseHeartsMainGameClass _mainGame;
    private readonly HuseHeartsVMData _model;
    private readonly IToast _toast;
    public override string Text => "Pass Cards";
    public PassingViewModel(CommandContainer commandContainer, 
        HuseHeartsMainGameClass mainGame,
        HuseHeartsVMData model, 
        IEventAggregator aggregator,
        IToast toast
        ) : base(commandContainer, aggregator)
    {
        _mainGame = mainGame;
        _model = model;
        _toast = toast;
    }
    public override bool CanSubmit => true;
    public override async Task SubmitAsync()
    {
        int cardsSelected = _model.PlayerHand1!.HowManySelectedObjects;
        if (cardsSelected != 3)
        {
            _toast.ShowUserErrorToast("Must pass 3 cards");
            return;
        }
        var tempList = _model.PlayerHand1.ListSelectedObjects().GetDeckListFromObjectList();
        if (_mainGame.BasicData!.MultiPlayer == true)
        {
            await _mainGame.Network!.SendAllAsync("passcards", tempList);
        }
        await _mainGame!.CardsPassedAsync(tempList);
    }
}
