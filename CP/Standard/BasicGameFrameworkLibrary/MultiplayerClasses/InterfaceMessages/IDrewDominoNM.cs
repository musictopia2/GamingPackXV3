namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
public interface IDrewDominoNM
{
    Task DrewDominoReceivedAsync(int deck);
}