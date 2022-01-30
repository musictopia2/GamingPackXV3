//i think this is the most common things i like to do
namespace MastermindBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<MastermindShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        MastermindCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<MastermindShellViewModel>(); //has to use interface part to make it work with source generators.
        MastermindCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        MastermindCP.JsonContextProcesses.GlobalJsonContextClass.AddJsonContexts(); //needs this as well.
    }
}