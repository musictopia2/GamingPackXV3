namespace GalaxyCardGameCP.Data;
[SingletonGame]
public class GalaxyCardGameSaveInfo : BasicSavedTrickGamesClass<EnumSuitList, GalaxyCardGameCardInformation, GalaxyCardGamePlayerItem>, IMappable, ISaveInfo
{
    public EnumGameStatus GameStatus { get; set; }
    public GalaxyCardGameCardInformation WinningCard { get; set; } = new GalaxyCardGameCardInformation();
    public int LastWin { get; set; }
}