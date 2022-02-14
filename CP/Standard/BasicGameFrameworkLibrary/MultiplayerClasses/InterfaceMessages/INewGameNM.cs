namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
public interface INewGameNM
{
    Task NewGameReceivedAsync(string data);
}