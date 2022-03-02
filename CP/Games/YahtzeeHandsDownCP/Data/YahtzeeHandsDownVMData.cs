namespace YahtzeeHandsDownCP.Data;
[SingletonGame]
[UseLabelGrid]
[AutoReset]
public partial class YahtzeeHandsDownVMData : IBasicCardGamesData<YahtzeeHandsDownCardInformation>
{
    [LabelColumn]
    public string NormalTurn { get; set; } = "";
    [LabelColumn]
    public string Status { get; set; } = "";
	public YahtzeeHandsDownVMData(CommandContainer command)
	{
		Deck1 = new (command);
		Pile1 = new (command);
		PlayerHand1 = new (command);
		ComboHandList = new (command);
		ComboHandList.Text = "Category Cards";
		ChancePile = new (command);
		ChancePile.Visible = false;
		ChancePile.CurrentOnly = true;
		ChancePile.Text = "Chance";
	}
	public HandObservable<ComboCardInfo>? ComboHandList;
	public SingleObservablePile<ChanceCardInfo>? ChancePile;
	public DeckObservablePile<YahtzeeHandsDownCardInformation> Deck1 { get; set; }
	public SingleObservablePile<YahtzeeHandsDownCardInformation> Pile1 { get; set; }
	public HandObservable<YahtzeeHandsDownCardInformation> PlayerHand1 { get; set; }
	public SingleObservablePile<YahtzeeHandsDownCardInformation>? OtherPile { get; set; }
	//any other ui related properties will be here.
	//can copy/paste for the actual view model.
}