namespace BowlingDiceGameBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BowlingDiceGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        BowlingDiceGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<BowlingDiceGamePlayerItem, BowlingDiceGameSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<BowlingDiceGamePlayerItem, BowlingDiceGameSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<BowlingDiceGamePlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BowlingDiceGameShellViewModel>(); //has to use interface part to make it work with source generators.
        BowlingDiceGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BowlingDiceGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}