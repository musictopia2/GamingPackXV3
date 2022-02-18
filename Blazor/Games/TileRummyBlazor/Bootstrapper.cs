namespace TileRummyBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<TileRummyShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        TileRummyCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<TileRummyPlayerItem, TileRummySaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<TileRummyPlayerItem, TileRummySaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<TileRummyPlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<TileRummyShellViewModel>(); //has to use interface part to make it work with source generators.
        TileRummyCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        TileRummyCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}