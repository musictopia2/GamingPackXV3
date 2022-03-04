namespace HuseHeartsBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<HuseHeartsShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.

        //change view model for area if not using 2 player.

        //if using misc deck, use this line
        //register.RegisterSingleton<IDeckCount, HuseHeartsDeckCount>();
        HuseHeartsCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        HuseHeartsCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        HuseHeartsCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        HuseHeartsCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<HuseHeartsShellViewModel>(); //has to use interface part to make it work with source generators.
        HuseHeartsCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        HuseHeartsCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}