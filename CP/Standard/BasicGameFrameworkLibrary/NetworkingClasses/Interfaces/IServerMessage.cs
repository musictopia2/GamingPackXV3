namespace BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
public interface IServerMessage
{
    Task ProcessDataAsync(SentMessage thisData);
}