namespace SixtySix2PlayerBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<SixtySix2PlayerShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.

        //change view model for area if not using 2 player.
        register.RegisterType<TwoPlayerTrickObservable<EnumSuitList, SixtySix2PlayerCardInformation, SixtySix2PlayerPlayerItem, SixtySix2PlayerSaveInfo>>();

        //if using misc deck, use this line
        register.RegisterSingleton<IDeckCount, CustomDeck>();
        SixtySix2PlayerCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        SixtySix2PlayerCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        SixtySix2PlayerCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        SixtySix2PlayerCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SixtySix2PlayerShellViewModel>(); //has to use interface part to make it work with source generators.
        SixtySix2PlayerCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        SixtySix2PlayerCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}