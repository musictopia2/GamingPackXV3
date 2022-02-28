namespace CrazyEightsBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<CrazyEightsShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        CrazyEightsCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        CrazyEightsCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        CrazyEightsCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        CrazyEightsCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<CrazyEightsShellViewModel>(); //has to use interface part to make it work with source generators.
        CrazyEightsCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        CrazyEightsCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}