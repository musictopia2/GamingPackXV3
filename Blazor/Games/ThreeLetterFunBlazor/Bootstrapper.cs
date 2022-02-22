namespace ThreeLetterFunBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ThreeLetterFunShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        ThreeLetterFunCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        ThreeLetterFunCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        ThreeLetterFunCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register.RegisterType<GenericCardShuffler<ThreeLetterFunCardData>>();
        register.RegisterSingleton<IDeckCount, ThreeLetterFunDeckInfo>();
        register.RegisterSingleton<ISpellingLogic, SpellingLogic>();
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