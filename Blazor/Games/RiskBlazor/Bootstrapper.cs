namespace RiskBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<RiskShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        RiskCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        RiskCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        RiskCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterStandardDice(GetDIContainer);
        RiskCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register.RegisterType<DrawShuffleClass<RiskCardInfo, RiskPlayerItem>>(); //hopefully this does not have to be replaced.
        register.RegisterType<GenericCardShuffler<RiskCardInfo>>(); //this is iffy too.
        register.RegisterSingleton<IDeckCount, RiskCardCount>();
        return Task.CompletedTask;
    }
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<RiskShellViewModel>(); //has to use interface part to make it work with source generators.
        RiskCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        RiskCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}