namespace Rummy500Blazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<Rummy500ShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        Rummy500CP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        Rummy500CP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        Rummy500CP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        Rummy500CP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<Rummy500ShellViewModel>(); //has to use interface part to make it work with source generators.
        Rummy500CP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        Rummy500CP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}