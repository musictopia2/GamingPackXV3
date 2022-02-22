namespace ConnectTheDotsBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ConnectTheDotsShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        ConnectTheDotsCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        ConnectTheDotsCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        ConnectTheDotsCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ConnectTheDotsShellViewModel>(); //has to use interface part to make it work with source generators.
        ConnectTheDotsCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ConnectTheDotsCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}