//i think this is the most common things i like to do

namespace CribbagePatienceBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<CribbagePatienceShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        CribbagePatienceCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<DeckObservablePile<CribbagePatienceCardInfo>>(true); //i think
        register.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
        register.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<CribbagePatienceShellViewModel>(); //has to use interface part to make it work with source generators.
        CribbagePatienceCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        CribbagePatienceCP.JsonContextProcesses.GlobalJsonContextClass.AddJsonContexts(); //needs this as well.
    }
}