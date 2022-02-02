//i think this is the most common things i like to do
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using SinglePlayerCardGamesCP.Data;

namespace SinglePlayerCardGamesBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<SinglePlayerCardGamesShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        SinglePlayerCardGamesCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<DeckObservablePile<SinglePlayerCardGamesCardInfo>>(true); //i think
        register.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
        register.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
        //anything that needs to be registered will be here.
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SinglePlayerCardGamesShellViewModel>(); //has to use interface part to make it work with source generators.
        SinglePlayerCardGamesCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        SinglePlayerCardGamesCP.JsonContextProcesses.GlobalJsonContextClass.AddJsonContexts(); //needs this as well.
    }
}