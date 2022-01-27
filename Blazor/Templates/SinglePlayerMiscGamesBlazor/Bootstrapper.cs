//i think this is the most common things i like to do
namespace SinglePlayerMiscGamesBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<SinglePlayerMiscGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync()
    {
        SinglePlayerMiscGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(OurContainer!);
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }
    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SinglePlayerMiscGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        SinglePlayerMiscGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(OurContainer!);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(OurContainer!);
        SinglePlayerMiscGamesCP.JsonContextProcesses.GlobalJsonContextClass.AddJsonContexts(); //needs this as well.
    }
}