
namespace KismetBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<YahtzeeShellViewModel<KismetDice>>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        KismetCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        KismetCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterBasicYahtzeeStyleClasses(GetDIContainer);
        KismetCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        KismetCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        KismetCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}