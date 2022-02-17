namespace ThreeLetterFunBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ThreeLetterFunShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        SpellingLogic.BaseAddress = GamingGlobalVariables.BaseAddress;
        ThreeLetterFunCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<ThreeLetterFunPlayerItem, ThreeLetterFunSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<ThreeLetterFunPlayerItem, ThreeLetterFunSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<ThreeLetterFunPlayerItem>>(true); //had to be set to true after all.
        register.RegisterType<GenericCardShuffler<ThreeLetterFunCardData>>();
        register.RegisterSingleton<IDeckCount, ThreeLetterFunDeckInfo>();
        register.RegisterSingleton<ISpellingLogic, SpellingLogic>();
        MiscDelegates.GetMiscObjectsToReplace = () =>
        {
            //if i have other types to register or even other assemblies; do here.
            return ThreeLetterFunCP.DIFinishProcesses.AutoResetClass.GetTypesToAutoReset();
        };
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
        SpellingBlazorLibrary.AutoResumeContexts.GlobalRegistrations.Register();
    }
}