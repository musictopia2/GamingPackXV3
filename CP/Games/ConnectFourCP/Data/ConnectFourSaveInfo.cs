namespace ConnectFourCP.Data;
[SingletonGame]
public class ConnectFourSaveInfo : BasicSavedGameClass<ConnectFourPlayerItem>, IMappable, ISaveInfo
{
    public ConnectFourCollection GameBoard { get; set; } = new ();
}