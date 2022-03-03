namespace CaliforniaJackBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<CaliforniaJackShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.

        //change view model for area if not using 2 player.
        register.RegisterType<TwoPlayerTrickObservable<EnumSuitList, CaliforniaJackCardInformation, CaliforniaJackPlayerItem, CaliforniaJackSaveInfo>>();

        //if using misc deck, use this line
        //register.RegisterSingleton<IDeckCount, CaliforniaJackDeckCount>();
        CaliforniaJackCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        CaliforniaJackCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        CaliforniaJackCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        CaliforniaJackCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<CaliforniaJackShellViewModel>(); //has to use interface part to make it work with source generators.
        CaliforniaJackCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        CaliforniaJackCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}