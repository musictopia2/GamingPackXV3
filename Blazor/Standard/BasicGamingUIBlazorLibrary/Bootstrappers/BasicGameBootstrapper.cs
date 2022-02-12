namespace BasicGamingUIBlazorLibrary.Bootstrappers;
public abstract partial class BasicGameBootstrapper<TViewModel> : IGameBootstrapper, IHandleAsync<SocketErrorEventModel>,
    IHandleAsync<DisconnectEventModel>
    where TViewModel : IMainGPXShellVM //needs generic so its able to do the part to active a screen if any.
{
    private readonly IStartUp? _startInfo;
    private readonly EnumGamePackageMode _mode;
    private ISystemError? _error;
    private IMessageBox? _message;
    public BasicGameBootstrapper(IStartUp starts, EnumGamePackageMode mode)
    {
        _startInfo = starts;
        _mode = mode;
        _aggregator = new EventAggregator();
        InitalizeAsync(); //maybe its okay to be async void.  doing the other is causing too many problems with the game package.
    }
    private partial void Subscribe();
    private partial void Unsubscribe();
    bool _isInitialized;
    //has to clear out until ready
    private static void ResetGlobals()
    {
        //MiscDelegates.GetMiscObjectsToReplace = null;
        //MiscDelegates.ColorsFinishedAsync = null; //needs to set all to null.  best to just do this way.
        //MiscDelegates.ComputerChooseColorsAsync = null;
        //MiscDelegates.ContinueColorsAsync = null;
        //MiscDelegates.FillRestColors = null;
        //MiscDelegates.ManuelSetColors = null;
        //if i find any other objects that got carried over, do as well (?)
        RegularSimpleCard.ClearSavedList(); //i think should be here instead.  the fact others do it should not hurt.  best to be safe than sorry.
    }
    public async void InitalizeAsync()
    {
        if (_isInitialized)
        {
            return;
        }
        _error = BasicBlazorLibrary.Helpers.BlazorUIHelpers.SystemError;
        _message = BasicBlazorLibrary.Helpers.BlazorUIHelpers.MessageBox;
        CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.SystemTextJsonStrings.RequireCustomSerialization = true; //for the entire game package requires custom serialization.
        ResetGlobals();
        _isInitialized = true;
        GlobalDelegates.RefreshSubscriptions = (a =>
        {
            Unsubscribe(); //do this too.
            EventAggravatorProcesses.GlobalEventAggravatorClass.ClearSubscriptions(_aggregator); //i think this first
            Subscribe(); //i think
        });
        await StartRuntimeAsync();
    }
    protected BasicData? GameData;
    protected TestOptions? TestData; //this is very important to have too.
    private readonly IEventAggregator _aggregator;

    /// <summary>
    /// Called by the bootstrapper's constructor at runtime to start the framework.
    /// </summary>
    protected async Task StartRuntimeAsync()
    {
        if (_mode == EnumGamePackageMode.None)
        {
            Console.WriteLine("Closing out because must be debug or production.");
            return;
        }
        StartUp(); //no operating system now.  if that changes, rethink.
        SetPersonalSettings(); //i think
        _container = new GamePackageDIContainer();
        aa.Resolver = _container;
        FirstRegister();
        await ConfigureAsync(_container);
        if (_mode == EnumGamePackageMode.Debug)
        {
            await RegisterTestsAsync();
        }
        if (UseMultiplayerProcesses)
        {
            throw new CustomBasicException("Can't start multiplayer processes yet");
            //OurContainer.RegisterType<BasicMessageProcessing>(true);
            //IRegisterNetworks tempnets = OurContainer.Resolve<IRegisterNetworks>();
            //tempnets.RegisterMultiplayerClasses(OurContainer); //since i commented out, maybe its okay now.
        }
        await DisplayRootViewForAsync(); //has to do here now.
    }
    protected virtual Task RegisterTestsAsync() { return Task.CompletedTask; }
    protected abstract bool UseMultiplayerProcesses { get; }
    protected virtual void StartUp() { }
    private void FirstRegister()
    {
        BasicGameFrameworkLibrary.AutoResumeContexts.GlobalRegistrations.Register(); //this is needed always no matter what.
        _container!.RegisterStartup(_startInfo!);
        _container.RegisterSingleton(BasicBlazorLibrary.Helpers.BlazorUIHelpers.Toast);
        _container.RegisterSingleton(_message);
        _container.RegisterSingleton(_error); //these 3 are always needed.  good thing i found a fix from doing the music player.
        EventAggregator thisEvent = new();
        MessengingGlobalClass.Aggregator = thisEvent;
        Subscribe(); //now i can use this.
        _container!.RegisterSingleton(thisEvent); //put to list so if anything else needs it, can get from the container.
        TestData = new TestOptions();
        _container.RegisterSingleton(TestData); //iffy.
        CommandContainer thisCommand = new();
        _container.RegisterSingleton(thisCommand);
        BasicRegistrations(_container); //has to be interface so di containers work properly.
        MiscRegisterFirst();
        _container.RegisterSingleton(_container);
        GameData = new ();
        GameData.GamePackageMode = _mode;
        _container.RegisterSingleton(GameData);
        _startInfo!.RegisterCustomClasses(_container, UseMultiplayerProcesses, GameData); //for now this way.  could change later.
    }
    private static void BasicRegistrations(IGamePackageRegister register)
    {
        register.RegisterType<NewGameViewModel>(false);
        register.RegisterSingleton<IAsyncDelayer, AsyncDelayer>();
    }
    protected virtual void MiscRegisterFirst() { }

    /// <summary>
    /// if we need custom registrations but still need standard, then override but do the regular functions too.
    /// </summary>
    /// <returns></returns>
    protected abstract Task ConfigureAsync(IGamePackageRegister register);
    private GamePackageDIContainer? _container;
    protected IGamePackageDIContainer GetDIContainer => _container!;
    protected virtual void SetPersonalSettings() { }
    /// <summary>
    /// this will allow source generators to run to finish the dependency injection registrations.
    /// </summary>
    protected abstract void FinishRegistrations(IGamePackageRegister register); //this will include registering the type.  (templates can do that).  which will allow source generators to do its job.
    //at this stage, all registrations should be done.  so anything that needs to run to finish should be done.
    protected async Task DisplayRootViewForAsync()
    {
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(_container!);
        FinishRegistrations(_container!);
        object item = _container!.Resolve<TViewModel>()!;
        if (item is IScreen screen)
        {
            await screen.ActivateAsync();
        }
    }
    protected virtual bool NeedExtraLocations { get; } = true;
    async Task IHandleAsync<SocketErrorEventModel>.HandleAsync(SocketErrorEventModel message)
    {
        if (message.Category == EnumSocketCategory.Client)
        {
            await _message!.ShowMessageAsync($"Client Socket Error. The message was {message.Message}");
        }
        else if (message.Category == EnumSocketCategory.Server)
        {
            await _message!.ShowMessageAsync($"Server Socket Error. The message was {message.Message}");
        }
        else
        {
            _error!.ShowSystemError("No Category Found For Socket Error");
        }
    }
    async Task IHandleAsync<DisconnectEventModel>.HandleAsync(DisconnectEventModel message)
    {

        await _message!.ShowMessageAsync("Disconnected.  May have to refresh which starts all over again");
    }
}