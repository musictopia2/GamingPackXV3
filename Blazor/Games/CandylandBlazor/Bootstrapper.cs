namespace CandylandBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<CandylandShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        CandylandCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<CandylandPlayerItem, CandylandSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<CandylandPlayerItem, CandylandSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<CandylandPlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<CandylandShellViewModel>(); //has to use interface part to make it work with source generators.
        CandylandCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        CandylandCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}