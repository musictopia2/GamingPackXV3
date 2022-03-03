namespace BasicMultiplayerTrickCardGamesBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BasicMultiplayerTrickCardGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        register.RegisterSingleton<IDeckCount, BasicMultiplayerTrickCardGamesDeckCount>();
        BasicMultiplayerTrickCardGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BasicMultiplayerTrickCardGamesCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        BasicMultiplayerTrickCardGamesCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BasicMultiplayerTrickCardGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        BasicMultiplayerTrickCardGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BasicMultiplayerTrickCardGamesCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}