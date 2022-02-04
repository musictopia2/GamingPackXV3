//i think this is the most common things i like to do

namespace TriangleSolitaireBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<TriangleSolitaireShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        TriangleSolitaireCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<DeckObservablePile<TriangleSolitaireCardInfo>>(true); //i think
        register.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
        register.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<TriangleSolitaireShellViewModel>(); //has to use interface part to make it work with source generators.
        TriangleSolitaireCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        TriangleSolitaireCP.JsonContextProcesses.GlobalJsonContextClass.AddJsonContexts(); //needs this as well.
    }
}