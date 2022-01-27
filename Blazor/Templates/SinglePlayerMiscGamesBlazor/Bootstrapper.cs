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

    protected override void FinishRegistrations()
    { 
        SinglePlayerMiscGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(OurContainer!);
        //any other processes to finish up for di registrations (?)

    }
}
