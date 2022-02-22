
namespace ItalianDominosBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ItalianDominosShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        ItalianDominosCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        ItalianDominosCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        ItalianDominosCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register.RegisterType<DominosBasicShuffler<SimpleDominoInfo>>(true);
        register.RegisterSingleton<IDeckCount, SimpleDominoInfo>(); //has to do this to stop overflow and duplicates bug.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ItalianDominosShellViewModel>(); //has to use interface part to make it work with source generators.
        ItalianDominosCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ItalianDominosCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}