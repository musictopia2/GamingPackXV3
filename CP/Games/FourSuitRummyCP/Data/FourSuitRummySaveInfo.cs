namespace FourSuitRummyCP.Data;
[SingletonGame]
public class FourSuitRummySaveInfo : BasicSavedCardClass<FourSuitRummyPlayerItem, RegularRummyCard>, IMappable, ISaveInfo
{
	public int TimesReshuffled { get; set; }
}