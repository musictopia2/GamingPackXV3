namespace DiceBoardGamesMultiplayerBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<DiceBoardGamesMultiplayerShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        DiceBoardGamesMultiplayerCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        DiceBoardGamesMultiplayerCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
		DiceBoardGamesMultiplayerCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterStandardDice(GetDIContainer);
        DiceBoardGamesMultiplayerCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<DiceBoardGamesMultiplayerShellViewModel>(); //has to use interface part to make it work with source generators.
        DiceBoardGamesMultiplayerCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DiceBoardGamesMultiplayerCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}