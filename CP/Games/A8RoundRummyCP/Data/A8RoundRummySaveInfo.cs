namespace A8RoundRummyCP.Data;
[SingletonGame]
public class A8RoundRummySaveInfo : BasicSavedCardClass<A8RoundRummyPlayerItem, A8RoundRummyCardInformation>, IMappable, ISaveInfo
{
	public BasicList<RoundClass> RoundList { get; set; } = new();
}