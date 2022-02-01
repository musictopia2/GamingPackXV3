//i think this is the most common things i like to do
namespace BuncoDiceGameBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<BuncoDiceGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        register!.RegisterSingleton<IGenerateDice<int>, SimpleDice>(); //hopefully this simple (?)
        BuncoDiceGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    //looks like does not need the di processes for the blazor library (at least not this time).
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<BuncoDiceGameShellViewModel>(); //has to use interface part to make it work with source generators.
        BuncoDiceGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        //BuncoDiceGameCP.JsonContextProcesses.GlobalJsonContextClass.AddJsonContexts(); //needs this as well.
    }
}