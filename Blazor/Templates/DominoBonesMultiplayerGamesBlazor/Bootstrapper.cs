
namespace DominoBonesMultiplayerGamesBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<DominoBonesMultiplayerGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        DominoBonesMultiplayerGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<DominoBonesMultiplayerGamesPlayerItem, DominoBonesMultiplayerGamesSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<DominoBonesMultiplayerGamesPlayerItem, DominoBonesMultiplayerGamesSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<DominoBonesMultiplayerGamesPlayerItem>>(true);
        register.RegisterType<DominosBasicShuffler<SimpleDominoInfo>>(true);
        register.RegisterSingleton<IDeckCount, SimpleDominoInfo>(); //has to do this to stop overflow and duplicates bug.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<DominoBonesMultiplayerGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        DominoBonesMultiplayerGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DominoBonesMultiplayerGamesCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}