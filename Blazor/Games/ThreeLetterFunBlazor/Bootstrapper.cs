namespace ThreeLetterFunBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ThreeLetterFunShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        ThreeLetterFunCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<ThreeLetterFunPlayerItem, ThreeLetterFunSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<ThreeLetterFunPlayerItem, ThreeLetterFunSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<ThreeLetterFunPlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ThreeLetterFunShellViewModel>(); //has to use interface part to make it work with source generators.
        ThreeLetterFunCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ThreeLetterFunCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}