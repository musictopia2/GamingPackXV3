//i think this is the most common things i like to do
namespace XPuzzleBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<XPuzzleShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        XPuzzleCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<XPuzzleShellViewModel>(); //has to use interface part to make it work with source generators.
        XPuzzleCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        XPuzzleCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}