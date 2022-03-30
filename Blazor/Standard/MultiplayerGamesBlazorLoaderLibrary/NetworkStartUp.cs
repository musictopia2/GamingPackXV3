namespace MultiplayerGamesBlazorLoaderLibrary;
public class NetworkStartUp : IRegisterNetworks
{
    void IRegisterNetworks.RegisterMultiplayerClasses(IGamePackageDIContainer container)
    {
        container.RegisterType<SignalRMessageService>();
        container.RegisterType<SignalRAzureEndPoint>();
        DIFinishProcesses.GlobalDIFinishClass.FinishDIRegistrations(container); //has to be here.  otherwise, the timing causes major issues.
    }
}