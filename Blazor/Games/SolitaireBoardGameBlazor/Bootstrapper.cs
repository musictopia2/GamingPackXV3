//i think this is the most common things i like to do
namespace SolitaireBoardGameBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<SolitaireBoardGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        SolitaireBoardGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SolitaireBoardGameShellViewModel>(); //has to use interface part to make it work with source generators.
        SolitaireBoardGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        SolitaireBoardGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}