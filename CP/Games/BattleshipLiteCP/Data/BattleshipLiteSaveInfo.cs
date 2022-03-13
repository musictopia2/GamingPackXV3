namespace BattleshipLiteCP.Data;
[SingletonGame]
public class BattleshipLiteSaveInfo : BasicSavedGameClass<BattleshipLitePlayerItem>, IMappable, ISaveInfo
{
    public EnumGameStatus GameStatus { get; set; }
}