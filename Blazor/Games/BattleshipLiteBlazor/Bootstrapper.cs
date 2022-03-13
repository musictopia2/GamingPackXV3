namespace BattleshipLiteBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BattleshipLiteShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        BattleshipLiteCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BattleshipLiteCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        BattleshipLiteCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }
    
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BattleshipLiteShellViewModel>(); //has to use interface part to make it work with source generators.
        BattleshipLiteCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BattleshipLiteCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}