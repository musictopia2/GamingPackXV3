using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;

namespace LottoDominosBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<LottoDominosShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        LottoDominosCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<LottoDominosPlayerItem, LottoDominosSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<LottoDominosPlayerItem, LottoDominosSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<LottoDominosPlayerItem>>(true); //had to be set to true after all.
        MiscDelegates.GetMiscObjectsToReplace = () =>
        {
            //if i have other types to register or even other assemblies; do here.
            return LottoDominosCP.DIFinishProcesses.AutoResetClass.GetTypesToAutoReset();
        };
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<LottoDominosShellViewModel>(); //has to use interface part to make it work with source generators.
        LottoDominosCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        LottoDominosCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}