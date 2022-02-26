namespace ClueBoardGameCP.Data;
[SingletonGame]
[UseLabelGrid]
public partial class ClueBoardGameVMData : IDiceBoardGamesData
{
    [LabelColumn]
    public string NormalTurn { get; set; } = "";
    [LabelColumn]
    public string Status { get; set; } = "";
    [LabelColumn]
    public string Instructions { get; set; } = "";
    [LabelColumn]
    public int LeftToMove { get; set; }
    [LabelColumn]
    public string CurrentRoomName { get; set; } = "";
    [LabelColumn]
    public string CurrentCharacterName { get; set; } = "";
    [LabelColumn]
    public string CurrentWeaponName { get; set; } = "";
    public DiceCup<SimpleDice>? Cup { get; set; }
    private readonly CommandContainer _command;
    private readonly IGamePackageResolver _resolver;
    public HandObservable<CardInfo> HandList;
    public SingleObservablePile<CardInfo> Pile;
    public ClueBoardGameVMData(CommandContainer command, IGamePackageResolver resolver, IEventAggregator aggregator)
    {
        _command = command;
        _resolver = resolver;
        HandList = new (command);
        HandList.AutoSelect = EnumHandAutoType.None;
        HandList.Maximum = 3;
        HandList.Text = "Your Cards";
        Pile = new (command);
        Pile.CurrentOnly = true;
        Pile.Text = "Clue";
    }
    public void LoadCup(ISavedDiceList<SimpleDice> saveRoot, bool autoResume)
	{
		if (Cup != null && autoResume)
		{
			return;
		}
		Cup = new DiceCup<SimpleDice>(saveRoot.DiceList, _resolver, _command);
		if (autoResume == true)
		{
			Cup.CanShowDice = true;
		}
		Cup.HowManyDice = 1; //you specify how many dice here.
		Cup.Visible = true; //i think.

	}
}