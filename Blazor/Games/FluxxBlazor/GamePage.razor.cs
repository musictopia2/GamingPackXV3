namespace FluxxBlazor;
public partial class GamePage
{
    private static int TargetHeight => 12;
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    [CascadingParameter]
    public IGameInfo? GameData { get; set; }
    [CascadingParameter]
    public BasicData? BasicData { get; set; }
    [CascadingParameter]
    public MultiplayerBasicParentShell? Shell { get; set; }
    private static CompleteContainerClass GetContainer => new ();
}