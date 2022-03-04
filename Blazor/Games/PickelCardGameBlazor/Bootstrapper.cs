namespace PickelCardGameBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<PickelCardGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.

        //change view model for area if not using 2 player.
        register.RegisterType<TwoPlayerTrickObservable<EnumSuitList, PickelCardGameCardInformation, PickelCardGamePlayerItem, PickelCardGameSaveInfo>>();

        //if using misc deck, use this line
        register.RegisterSingleton<IDeckCount, CustomDeck>();
        PickelCardGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        PickelCardGameCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        PickelCardGameCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        PickelCardGameCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<PickelCardGameShellViewModel>(); //has to use interface part to make it work with source generators.
        PickelCardGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        PickelCardGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}