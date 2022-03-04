namespace GalaxyCardGameBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<GalaxyCardGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.

        //change view model for area if not using 2 player.
        register.RegisterType<TwoPlayerTrickObservable<EnumSuitList, GalaxyCardGameCardInformation, GalaxyCardGamePlayerItem, GalaxyCardGameSaveInfo>>();

        //if using misc deck, use this line
        //register.RegisterSingleton<IDeckCount, GalaxyCardGameDeckCount>();
        GalaxyCardGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        GalaxyCardGameCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        GalaxyCardGameCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        GalaxyCardGameCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<GalaxyCardGameShellViewModel>(); //has to use interface part to make it work with source generators.
        GalaxyCardGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        GalaxyCardGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}