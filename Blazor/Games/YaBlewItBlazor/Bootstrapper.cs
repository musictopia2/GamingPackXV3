namespace YaBlewItBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<YaBlewItShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        register.RegisterSingleton<IDeckCount, YaBlewItDeckCount>();
        YaBlewItCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        YaBlewItCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        YaBlewItCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }
    //protected override Task RegisterTestsAsync()
    //{
    //    TestData!.SaveOption = EnumTestSaveCategory.RestoreOnly;
    //    return base.RegisterTestsAsync();
    //}
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<YaBlewItShellViewModel>(); //has to use interface part to make it work with source generators.
        register.RegisterType<StandardRollProcesses<EightSidedDice, YaBlewItPlayerItem>>();
        register.RegisterSingleton<IGenerateDice<int>, EightSidedDice>();
        YaBlewItCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        YaBlewItCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}