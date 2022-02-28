namespace OldMaidBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<OldMaidShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        register.RegisterSingleton<IDeckCount, OldMaidDeck>();
        OldMaidCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        OldMaidCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        OldMaidCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        OldMaidCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<OldMaidShellViewModel>(); //has to use interface part to make it work with source generators.
        OldMaidCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        OldMaidCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}