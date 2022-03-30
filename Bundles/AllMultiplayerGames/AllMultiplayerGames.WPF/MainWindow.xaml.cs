namespace AllMultiplayerGames.WPF;
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
        serviceCollection.RegisterDefaultMultiplayerProcesses<BasicViewModel>();
        Resources.Add("services", serviceCollection.BuildServiceProvider());
        InitializeComponent();
    }
}