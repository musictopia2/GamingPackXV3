namespace BasicMultiplayerGamesBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        BasicMultiplayerGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BasicMultiplayerGamesCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        BasicMultiplayerGamesCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BasicMultiplayerGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        BasicMultiplayerGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BasicMultiplayerGamesCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}