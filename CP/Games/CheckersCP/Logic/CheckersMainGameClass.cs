using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
namespace CheckersCP.Logic;
[SingletonGame]
public class CheckersMainGameClass
    : SimpleBoardGameClass<CheckersPlayerItem, CheckersSaveInfo, EnumColorChoice, int>
    , IBeginningColors<EnumColorChoice, CheckersPlayerItem, CheckersSaveInfo>
    , IMiscDataNM, ISerializable
{
    public CheckersMainGameClass(IGamePackageResolver resolver,
        IEventAggregator aggregator,
        BasicData basic,
        TestOptions test,
        CheckersVMData model,
        IMultiplayerSaveState state,
        IAsyncDelayer delay,
        CommandContainer command,
        CheckersGameContainer gameContainer,
        ISystemError error,
        IToast toast
        ) : base(resolver, aggregator, basic, test, model, state, delay, command, gameContainer, error, toast)
    {
        _model = model;
    }

    private readonly CheckersVMData? _model;

    public override Task FinishGetSavedAsync()
    {
        LoadControls();
        BoardGameSaved(); //i think.
        //anything else needed is here.
        return Task.CompletedTask;
    }
    private void LoadControls()
    {
        if (IsLoaded == true)
        {
            return;
        }

        IsLoaded = true; //i think needs to be here.
    }
    protected override async Task ComputerTurnAsync()
    {
        //if there is nothing, then just won't do anything.
        await Task.CompletedTask;
    }
    public override async Task SetUpGameAsync(bool isBeginning)
    {
        LoadControls();
        if (FinishUpAsync == null)
        {
            throw new CustomBasicException("The loader never set the finish up code.  Rethink");
        }
        SaveRoot!.ImmediatelyStartTurn = true; //most of the time, needs to immediately start turn.  if i am wrong, rethink.
        await FinishUpAsync(isBeginning);
    }
    Task IMiscDataNM.MiscDataReceived(string status, string content)
    {
        switch (status) //can't do switch because we don't know what the cases are ahead of time.
        {
            //put in cases here.

            default:
                throw new CustomBasicException($"Nothing for status {status}  with the message of {content}");
        }
    }
    public override async Task StartNewTurnAsync()
    {
        if (PlayerList.DidChooseColors())
        {
            PrepStartTurn();
        }

        await ContinueTurnAsync(); //most of the time, continue turn.  can change to what is needed
    }
    public override Task ContinueTurnAsync()
    {
        if (PlayerList.DidChooseColors())
        {
            //can do extra things upon continue turn.  many board games require other things.

        }
        return base.ContinueTurnAsync();
    }
    public override async Task MakeMoveAsync(int space)
    {
        //well see what we need for the move.
        await Task.CompletedTask;
    }
    public override async Task EndTurnAsync()
    {
        WhoTurn = await PlayerList!.CalculateWhoTurnAsync();
        //if anything else is needed, do here.
        if (PlayerList.DidChooseColors())
        {
            //can do extra things upon ending turn.  many board games require other things. only do if the player actually chose colors.

        }
        await StartNewTurnAsync();
    }
    public override async Task AfterChoosingColorsAsync()
    {
        //anything else that is needed after they finished choosing colors.

        await EndTurnAsync();
    }
}