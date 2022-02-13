namespace EagleWingsSolitaireBlazor;
public class Bootstrapper : SinglePlayerBootstrapper<EagleWingsSolitaireShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    

    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        EagleWingsSolitaireCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
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
        register.RegisterType<EagleWingsSolitaireShellViewModel>(); //has to use interface part to make it work with source generators.
        EagleWingsSolitaireCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        EagleWingsSolitaireCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}