namespace BasicMultiplayerMiscCardGamesBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerMiscCardGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        register.RegisterSingleton<IDeckCount, BasicMultiplayerMiscCardGamesDeckCount>();
        BasicMultiplayerMiscCardGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BasicMultiplayerMiscCardGamesCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        BasicMultiplayerMiscCardGamesCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BasicMultiplayerMiscCardGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        BasicMultiplayerMiscCardGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BasicMultiplayerMiscCardGamesCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}