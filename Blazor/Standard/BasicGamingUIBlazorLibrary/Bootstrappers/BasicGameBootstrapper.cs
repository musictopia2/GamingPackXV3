namespace BasicGamingUIBlazorLibrary.Bootstrappers;
public abstract partial class BasicGameBootstrapper<TViewModel> : IGameBootstrapper, IHandleAsync<SocketErrorEventModel>,
    IHandleAsync<DisconnectEventModel>
     where TViewModel : IMainGPXShellVM
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
    //private static void ResetGlobals()
    //{
    //    MiscDelegates.GetMiscObjectsToReplace = null;
    //    MiscDelegates.ColorsFinishedAsync = null; //needs to set all to null.  best to just do this way.
    //    MiscDelegates.ComputerChooseColorsAsync = null;
    //    MiscDelegates.ContinueColorsAsync = null;
    //    MiscDelegates.FillRestColors = null;
    //    MiscDelegates.ManuelSetColors = null;
    //    //if i find any other objects that got carried over, do as well (?)
    //    RegularSimpleCard.ClearSavedList(); //i think should be here instead.  the fact others do it should not hurt.  best to be safe than sorry.
    //}
    public async void InitalizeAsync()
    {
        if (_isInitialized)
        {
            return;
        }
        _error = BasicBlazorLibrary.Helpers.BlazorUIHelpers.SystermError;
        _message = BasicBlazorLibrary.Helpers.BlazorUIHelpers.MessageBox;
        //ResetGlobals();
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
        OurContainer = new GamePackageDIContainer();
        aa.Resolver = OurContainer;
        FirstRegister();
        await ConfigureAsync();
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
        OurContainer!.RegisterStartup(_startInfo!);
        EventAggregator thisEvent = new();
        MessengingGlobalClass.Aggregator = thisEvent;
        Subscribe(); //now i can use this.
        OurContainer!.RegisterSingleton(thisEvent); //put to list so if anything else needs it, can get from the container.
        TestData = new TestOptions();
        OurContainer.RegisterSingleton(TestData); //iffy.
        CommandContainer thisCommand = new();
        OurContainer.RegisterSingleton(thisCommand);
        OurContainer.RegisterType<NewGameViewModel>(false); //bad news is its not working anyways.
        MiscRegisterFirst();
        OurContainer.RegisterSingleton(OurContainer);
        OurContainer.RegisterSingleton<IAsyncDelayer, AsyncDelayer>(); //for testing, will use a mock version.
        GameData = new ();
        GameData.GamePackageMode = _mode;
        OurContainer.RegisterSingleton(GameData);
        _startInfo!.RegisterCustomClasses(OurContainer, UseMultiplayerProcesses, GameData); //for now this way.  could change later.
    }
    protected virtual void MiscRegisterFirst() { }

    /// <summary>
    /// if we need custom registrations but still need standard, then override but do the regular functions too.
    /// </summary>
    /// <returns></returns>
    protected abstract Task ConfigureAsync();
    protected GamePackageDIContainer? OurContainer;
    protected virtual void SetPersonalSettings() { }
    /// <summary>
    /// this will allow source generators to run to finish the dependency injection registrations.
    /// </summary>
    protected abstract void FinishRegistrations(IGamePackageRegister register); //this will include registering the type.  (templates can do that).  which will allow source generators to do its job.
    //at this stage, all registrations should be done.  so anything that needs to run to finish should be done.
    protected async Task DisplayRootViewForAsync()
    {
        //OurContainer!.RegisterType<TViewModel>(true);
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(OurContainer);
        FinishRegistrations(OurContainer!);
        object item = OurContainer!.Resolve<TViewModel>()!;
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