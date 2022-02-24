namespace YahtzeeBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<YahtzeeShellViewModel<SimpleDice>>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        YahtzeeCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        YahtzeeCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterBasicYahtzeeStyleClasses(GetDIContainer);
        YahtzeeCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        YahtzeeCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        YahtzeeCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}