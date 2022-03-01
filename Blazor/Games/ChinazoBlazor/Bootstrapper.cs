namespace ChinazoBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ChinazoShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        register.RegisterSingleton<IDeckCount, CustomDeck>();
        ChinazoCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        ChinazoCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        ChinazoCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        ChinazoCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ChinazoShellViewModel>(); //has to use interface part to make it work with source generators.
        ChinazoCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ChinazoCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}