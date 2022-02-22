namespace ConnectTheDotsCP.Data;
[SingletonGame]
public class ConnectTheDotsSaveInfo : BasicSavedGameClass<ConnectTheDotsPlayerItem>, IMappable, ISaveInfo
{
    public SavedBoardData? BoardData { get; set; }
}