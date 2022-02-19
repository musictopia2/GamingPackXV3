
namespace DominosRegularBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<DominosRegularShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        DominosRegularCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<DominosRegularPlayerItem, DominosRegularSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<DominosRegularPlayerItem, DominosRegularSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<DominosRegularPlayerItem>>(true);
        register.RegisterType<DominosBasicShuffler<SimpleDominoInfo>>(true);
        register.RegisterSingleton<IDeckCount, SimpleDominoInfo>(); //has to do this to stop overflow and duplicates bug.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<DominosRegularShellViewModel>(); //has to use interface part to make it work with source generators.
        DominosRegularCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DominosRegularCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}