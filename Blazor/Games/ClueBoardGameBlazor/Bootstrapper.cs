namespace ClueBoardGameBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ClueBoardGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        ClueBoardGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        ClueBoardGameCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
		ClueBoardGameCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterStandardDice(GetDIContainer);
        ClueBoardGameCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ClueBoardGameShellViewModel>(); //has to use interface part to make it work with source generators.
        ClueBoardGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ClueBoardGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}