namespace BattleshipBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<BattleshipShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        BattleshipCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        BattleshipCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //register!.RegisterType<BasicGameLoader<BattleshipPlayerItem, BattleshipSaveInfo>>();
        //register.RegisterType<RetrieveSavedPlayers<BattleshipPlayerItem, BattleshipSaveInfo>>();
        //register.RegisterType<MultiplayerOpeningViewModel<BattleshipPlayerItem>>(true); //had to be set to true after all.
        BattleshipCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        //MiscDelegates.GetMiscObjectsToReplace = () =>
        //{
        //    //if i have other types to register or even other assemblies; do here.
        //    return BattleshipCP.DIFinishProcesses.AutoResetClass.GetTypesToAutoReset();
        //};
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BattleshipShellViewModel>(); //has to use interface part to make it work with source generators.
        BattleshipCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        BattleshipCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}