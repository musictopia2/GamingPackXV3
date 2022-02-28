namespace BladesOfSteelBlazor;
public partial class GamePage
{
    private int TargetHeight => 15;
    [CascadingParameter]
    public TestOptions? TestData { get; set; }
    [CascadingParameter]
    public IGameInfo? GameData { get; set; }
    [CascadingParameter]
    public BasicData? BasicData { get; set; }
    [CascadingParameter]
    public MultiplayerBasicParentShell? Shell { get; set; }
    private static BladesOfSteelVMData? GetData => aa.Resolver!.Resolve<BladesOfSteelVMData>();
}