using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;

namespace MancalaBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<MancalaShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        MancalaCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<MancalaPlayerItem, MancalaSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<MancalaPlayerItem, MancalaSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<MancalaPlayerItem>>(true); //had to be set to true after all.
        MiscDelegates.GetMiscObjectsToReplace = () =>
        {
            //if i have other types to register or even other assemblies; do here.
            return MancalaCP.DIFinishProcesses.AutoResetClass.GetTypesToAutoReset();
        };
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<MancalaShellViewModel>(); //has to use interface part to make it work with source generators.
        MancalaCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        MancalaCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}