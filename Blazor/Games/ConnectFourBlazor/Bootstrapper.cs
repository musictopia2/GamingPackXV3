namespace ConnectFourBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ConnectFourShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        ConnectFourCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        ConnectFourCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        ConnectFourCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ConnectFourShellViewModel>(); //has to use interface part to make it work with source generators.
        ConnectFourCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ConnectFourCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}