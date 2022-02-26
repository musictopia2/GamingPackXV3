namespace ClueBoardGameBlazor;
public partial class GamePage
{
    public int TargetHeight { get; set; } = 17; //can adjust as needed;
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