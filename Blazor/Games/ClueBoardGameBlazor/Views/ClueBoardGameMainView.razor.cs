namespace ClueBoardGameBlazor.Views;
public partial class ClueBoardGameMainView
{
    private readonly BasicList<LabelGridModel> _labels = new();
    private readonly BasicList<LabelGridModel> _clues = new();
    private GameBoardGraphicsCP? _graphicsData;
    protected override void OnInitialized()
    {
        _graphicsData = aa.Resolver!.Resolve<GameBoardGraphicsCP>();
        _labels.Clear();
        _labels.AddLabel("Turn", nameof(ClueBoardGameVMData.NormalTurn));
        _clues.Clear();
        _clues.AddLabel("Room", nameof(ClueBoardGameVMData.CurrentRoomName))
            .AddLabel("Character", nameof(ClueBoardGameVMData.CurrentCharacterName))
            .AddLabel("Weapon", nameof(ClueBoardGameVMData.CurrentWeaponName));
        DataContext!.PopulateDetectiveNoteBook();
        base.OnInitialized();
    }
    private ICustomCommand EndCommand => DataContext!.EndTurnCommand!;
    private ICustomCommand RollCommand => DataContext!.RollDiceCommand!;
    private ICustomCommand PredictCommand => DataContext!.MakePredictionCommand!;
    private ICustomCommand AccusationCommand => DataContext!.MakeAccusationCommand!;
    private string GetColor()
    {
        var player = _graphicsData!.GameContainer!.PlayerList!.GetWhoPlayer();
        return player.Color.Color;
    }
    //private string GetColor => _graphicsData!.GameContainer!.SingleInfo!.Color.Color;
}