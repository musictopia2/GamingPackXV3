namespace SorryBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<SorryShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        SorryCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        SorryCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        SorryCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register!.RegisterType<DrawShuffleClass<CardInfo, SorryPlayerItem>>();
        register!.RegisterType<GenericCardShuffler<CardInfo>>();
        register!.RegisterSingleton<IDeckCount, DeckCount>();
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<SorryShellViewModel>(); //has to use interface part to make it work with source generators.
        SorryCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        SorryCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}