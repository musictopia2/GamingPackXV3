namespace PlainBoardGamesMultiplayerBlazor;
public class Bootstrapper : MultiplayerBasicBootstrapper<PlainBoardGamesMultiplayerShellViewModel>
{
    public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
    {
    }
    protected override Task ConfigureAsync(IGamePackageRegister register)
    {
        PlainBoardGamesMultiplayerCP.DIFinishProcesses.GlobalDIAutoRegisterClass.RegisterNonSavedClasses(GetDIContainer);
        register!.RegisterType<BasicGameLoader<PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>>();
        register.RegisterType<RetrieveSavedPlayers<PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>>();
        register.RegisterType<MultiplayerOpeningViewModel<PlainBoardGamesMultiplayerPlayerItem>>(true); //had to be set to true after all.
        register.RegisterType<BeginningColorProcessorClass<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>>();
        register.RegisterType<BeginningChooseColorViewModel<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem>>();
        register.RegisterType<BeginningColorModel<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem>>();
        //anything that needs to be registered will be here.
        MiscDelegates.GetMiscObjectsToReplace = () =>
        {
            BasicList<Type> output = new()
            {
                typeof(BeginningColorProcessorClass<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem, PlainBoardGamesMultiplayerSaveInfo>),
                typeof(BeginningColorModel<EnumColorChoice, PlainBoardGamesMultiplayerPlayerItem>)
            };
            return output;
        };
        return Task.CompletedTask;
    }

    //this part should not change
    protected override void FinishRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<PlainBoardGamesMultiplayerShellViewModel>(); //has to use interface part to make it work with source generators.
        PlainBoardGamesMultiplayerCP.DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(GetDIContainer);
        PlainBoardGamesMultiplayerCP.AutoResumeContexts.GlobalRegistrations.Register();
    }
}