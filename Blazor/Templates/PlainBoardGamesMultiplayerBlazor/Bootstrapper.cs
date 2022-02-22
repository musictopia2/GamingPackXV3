namespace PlainBoardGamesMultiplayerBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<PlainBoardGamesMultiplayerShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        PlainBoardGamesMultiplayerCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        PlainBoardGamesMultiplayerCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        PlainBoardGamesMultiplayerCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<PlainBoardGamesMultiplayerShellViewModel>(); //has to use interface part to make it work with source generators.
        PlainBoardGamesMultiplayerCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        PlainBoardGamesMultiplayerCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}