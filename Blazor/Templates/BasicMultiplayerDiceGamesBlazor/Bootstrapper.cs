namespace BasicMultiplayerDiceGamesBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerDiceGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        BasicMultiplayerDiceGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BasicMultiplayerDiceGamesCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        BasicMultiplayerDiceGamesCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BasicMultiplayerDiceGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        BasicMultiplayerDiceGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BasicMultiplayerDiceGamesCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}