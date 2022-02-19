namespace ConnectFourBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<ConnectFourShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        ConnectFourCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<ConnectFourPlayerItem, ConnectFourSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<ConnectFourPlayerItem, ConnectFourSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<ConnectFourPlayerItem>>(true); //had to be set to true after all.
        register.RegisterType<BeginningColorProcessorClass<EnumColorChoice, ConnectFourPlayerItem, ConnectFourSaveInfo>>();
        register.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, ConnectFourPlayerItem>>();
        register.RegisterType<BeginningColorModel<EnumColorChoice, ConnectFourPlayerItem>>();
        //anything that needs to be registered will be here.
        MiscDelegates.GetMiscObjectsToReplace = () =>
        {
            BasicList<Type> output = new()
            {
                typeof(BeginningColorProcessorClass<EnumColorChoice, ConnectFourPlayerItem, ConnectFourSaveInfo>),
                typeof(BeginningColorModel<EnumColorChoice, ConnectFourPlayerItem>)
            };
            return output;
        };
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<ConnectFourShellViewModel>(); //has to use interface part to make it work with source generators.
        ConnectFourCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        ConnectFourCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}