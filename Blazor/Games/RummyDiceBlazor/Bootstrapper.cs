namespace RummyDiceBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<RummyDiceShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        RummyDiceCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<RummyDicePlayerItem, RummyDiceSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<RummyDicePlayerItem, RummyDiceSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<RummyDicePlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<RummyDiceShellViewModel>(); //has to use interface part to make it work with source generators.
        RummyDiceCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        RummyDiceCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}