namespace BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
public interface IReconnectClientClass
{
    Task ReconnectClientAsync(string nickName);
}