namespace AlternationSolitaireBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<AlternationSolitaireShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        AlternationSolitaireCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<DeckObservablePile<SolitaireCard>>(true); //i think
        register.RegisterSingleton<IDeckCount, CustomDeck>(); //forgot to use a custom deck for this one.
        register.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //most of the time, aces are low.
        //anything that needs to be registered will be here.
        //we have to resolve the IMain and IWaste.
        register.RegisterType<WastePiles>();
        register.RegisterType<MainPilesCP>();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<AlternationSolitaireShellViewModel>(); //has to use interface part to make it work with source generators.
        AlternationSolitaireCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        AlternationSolitaireCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}