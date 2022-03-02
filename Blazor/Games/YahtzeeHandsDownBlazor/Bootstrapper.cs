namespace YahtzeeHandsDownBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<YahtzeeHandsDownShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        register.RegisterSingleton<IDeckCount, YahtzeeHandsDownDeckCount>();
        YahtzeeHandsDownCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        YahtzeeHandsDownCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        YahtzeeHandsDownCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<YahtzeeHandsDownShellViewModel>(); //has to use interface part to make it work with source generators.
        YahtzeeHandsDownCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        YahtzeeHandsDownCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}