namespace SnakesAndLaddersBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<SnakesAndLaddersShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        SnakesAndLaddersCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<SnakesAndLaddersPlayerItem, SnakesAndLaddersSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<SnakesAndLaddersPlayerItem, SnakesAndLaddersSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<SnakesAndLaddersPlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SnakesAndLaddersShellViewModel>(); //has to use interface part to make it work with source generators.
        SnakesAndLaddersCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        SnakesAndLaddersCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}