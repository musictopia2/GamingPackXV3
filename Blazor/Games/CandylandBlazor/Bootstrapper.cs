
namespace CandylandBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<CandylandShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        CandylandCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        CandylandCP.DIFinishProcesses.SpecializedRegistrationHelpers.RegisterCommonMultplayerClasses(GetDIContainer);
        CandylandCP.DIFinishProcesses.AutoResetClass.RegisterAutoResets();
        register.RegisterType<DrawShuffleClass<CandylandCardData, CandylandPlayerItem>>(); //hopefully this does not have to be replaced.
        register.RegisterType<GenericCardShuffler<CandylandCardData>>(); //this is iffy too.
        register.RegisterSingleton<IDeckCount, CandylandCount>();
        
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<CandylandShellViewModel>(); //has to use interface part to make it work with source generators.
        CandylandCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        CandylandCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}