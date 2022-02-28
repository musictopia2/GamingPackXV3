
namespace GolfCardGameBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<GolfCardGameShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        IBasicDiceGamesData<SimpleDice>.NeedsRollIncrement = true; //default to true.
        GolfCardGameCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        GolfCardGameCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        GolfCardGameCP.DIFinishProcesses.SpecializedRegularCardHelpers.RegisterRegularDeckOfCardClasses(GetDIContainer);
        GolfCardGameCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register.RegisterSingleton<IDeckCount, GolfDeck>();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<GolfCardGameShellViewModel>(); //has to use interface part to make it work with source generators.
        GolfCardGameCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        GolfCardGameCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}