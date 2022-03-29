namespace MultiplayerGamesBlazorLoaderLibrary;
public class NetworkStartUp : IRegisterNetworks
{
    void IRegisterNetworks.RegisterMultiplayerClasses(IGamePackageDIContainer container)
    {
        container.RegisterType<SignalRMessageService>();
        container.RegisterType<SignalRAzureEndPoint>();
    }
}