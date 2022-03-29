namespace NonCardOrSolitaireGames.WPF;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        ss.IsWasm = false;
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBlazorWebView();
		serviceCollection.RegisterWPFServices();
        serviceCollection.RegisterBlazorBeginningClasses();
        LoaderGlobalClass.LoadSettingsAsync = GlobalSettingsExtensions.LoadGlobalDataAsync;
        LoaderGlobalClass.SaveSettingsAsync = GlobalSettingsExtensions.SaveGlobalDataAsync;
        serviceCollection.RegisterDefaultSinglePlayerProcesses<BasicViewModel>();
        //here is where i do other services.
        //if i need to change title from view model, etc, rethink.
        Resources.Add("services", serviceCollection.BuildServiceProvider());
        InitializeComponent();
    }
}