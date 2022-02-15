namespace TicTacToeBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<TicTacToeShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        TicTacToeCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<TicTacToePlayerItem, TicTacToeSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<TicTacToePlayerItem, TicTacToeSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<TicTacToePlayerItem>>(true); //had to be set to true after all.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<TicTacToeShellViewModel>(); //has to use interface part to make it work with source generators.
        TicTacToeCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        TicTacToeCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}