namespace DominosMexicanTrainBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<DominosMexicanTrainShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        DominosMexicanTrainCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        DominosMexicanTrainCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        DominosMexicanTrainCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register.RegisterType<DominosBasicShuffler<MexicanDomino>>(true);
        register.RegisterSingleton<IDeckCount, MexicanDomino>(); //has to do this to stop overflow and duplicates bug.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<DominosMexicanTrainShellViewModel>(); //has to use interface part to make it work with source generators.
        DominosMexicanTrainCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DominosMexicanTrainCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}