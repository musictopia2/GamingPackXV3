namespace BasicMultiplayerRegularCardGamesBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerRegularCardGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        BasicMultiplayerRegularCardGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BasicMultiplayerRegularCardGamesCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        BasicMultiplayerRegularCardGamesCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        BasicMultiplayerRegularCardGamesCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BasicMultiplayerRegularCardGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        BasicMultiplayerRegularCardGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BasicMultiplayerRegularCardGamesCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}