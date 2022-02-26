namespace PaydayBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<PaydayShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        PaydayCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        PaydayCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
		PaydayCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterStandardDice(GetDIContainer);
        PaydayCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<PaydayShellViewModel>(); //has to use interface part to make it work with source generators.
        PaydayCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        PaydayCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}