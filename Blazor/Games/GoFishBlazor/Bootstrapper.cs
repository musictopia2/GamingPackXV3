namespace GoFishBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<GoFishShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        GoFishCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        GoFishCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        GoFishCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        GoFishCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<GoFishShellViewModel>(); //has to use interface part to make it work with source generators.
        GoFishCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        GoFishCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}