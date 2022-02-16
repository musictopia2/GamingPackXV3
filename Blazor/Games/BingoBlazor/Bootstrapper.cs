namespace BingoBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BingoShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        BingoCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<BingoPlayerItem, BingoSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<BingoPlayerItem, BingoSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<BingoPlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BingoShellViewModel>(); //has to use interface part to make it work with source generators.
        BingoCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BingoCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}