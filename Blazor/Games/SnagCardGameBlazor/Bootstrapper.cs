namespace SnagCardGameBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<SnagCardGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.


        //if using misc deck, use this line
        //register.RegisterSingleton<IDeckCount, SnagCardGameDeckCount>();
        SnagCardGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        SnagCardGameCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        //if using misc deck, then remove this line of code.
        SnagCardGameCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        SnagCardGameCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SnagCardGameShellViewModel>(); //has to use interface part to make it work with source generators.
        SnagCardGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        SnagCardGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}