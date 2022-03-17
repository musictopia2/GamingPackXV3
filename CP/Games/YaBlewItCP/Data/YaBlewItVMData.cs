namespace YaBlewItCP.Data;
[SingletonGame]
[UseLabelGrid]
[AutoReset]
public partial class YaBlewItVMData : IBasicCardGamesData<YaBlewItCardInformation>
{
    [LabelColumn]
    public string NormalTurn { get; set; } = "";
    [LabelColumn]
    public string Status { get; set; } = "";
	public YaBlewItVMData(CommandContainer command)
	{
		Deck1 = new (command);
		Pile1 = new (command);
		PlayerHand1 = new (command);
	}
	public DeckObservablePile<YaBlewItCardInformation> Deck1 { get; set; }
	public SingleObservablePile<YaBlewItCardInformation> Pile1 { get; set; }
	public HandObservable<YaBlewItCardInformation> PlayerHand1 { get; set; }
	public SingleObservablePile<YaBlewItCardInformation>? OtherPile { get; set; }
	//any other ui related properties will be here.
	//can copy/paste for the actual view model.
}