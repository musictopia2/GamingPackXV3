namespace SixtySix2PlayerBlazor;
public partial class GuideUI
{
    [Parameter]
    public SixtySix2PlayerVMData? GameData { get; set; }
    private BasicList<ScoreValuePair> _scores = new();
    protected override void OnInitialized()
    {
        _scores = SixtySix2PlayerVMData.GetDescriptionList();
        base.OnInitialized();
    }
}