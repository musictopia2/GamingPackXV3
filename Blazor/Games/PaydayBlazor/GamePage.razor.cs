namespace PaydayBlazor;
public partial class GamePage
{
    private readonly int _targetImageHeight = 13; //can adjust here.
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    [CascadingParameter]
    public IGameInfo? GameData { get; set; }
    [CascadingParameter]
    public BasicData? BasicData { get; set; }
    [CascadingParameter]
    public MultiplayerBasicParentShell? Shell { get; set; }
    private static string GetColor(EnumColorChoice color) => color.Color;
}