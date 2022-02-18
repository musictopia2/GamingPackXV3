namespace TileRummyBlazor;
public partial class GamePage
{
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    [CascadingParameter]
    public IGameInfo? GameData { get; set; }
    [CascadingParameter]
    public BasicData? BasicData { get; set; }
    public int TargetImageHeight { get; set; } = 6;
    [CascadingParameter]
    public MultiplayerBasicParentShell? Shell { get; set; }
}