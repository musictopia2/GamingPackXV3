namespace Spades2PlayerBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<Spades2PlayerShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.

        //change view model for area if not using 2 player.
        register.RegisterType<TwoPlayerTrickObservable<EnumSuitList, Spades2PlayerCardInformation, Spades2PlayerPlayerItem, Spades2PlayerSaveInfo>>();

        //if using misc deck, use this line
        //register.RegisterSingleton<IDeckCount, Spades2PlayerDeckCount>();
        Spades2PlayerCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        Spades2PlayerCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        Spades2PlayerCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        Spades2PlayerCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<Spades2PlayerShellViewModel>(); //has to use interface part to make it work with source generators.
        Spades2PlayerCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        Spades2PlayerCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}