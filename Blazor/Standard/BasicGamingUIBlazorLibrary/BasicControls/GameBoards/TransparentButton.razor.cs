namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards;
public partial class TransparentButton
{
    [Parameter]
    public EventCallback ButtonClicked { get; set; }
    [Parameter]
    public SizeF ButtonSize { get; set; }
    [Parameter]
    public PointF ButtonLocation { get; set; }
    [Parameter]
    public string BorderColor { get; set; } = cc.Black;
    [Parameter]
    public string TextColor { get; set; } = cc.Black;
    [Parameter]
    public int BorderWidth { get; set; } = 2;
    [Parameter]
    public string Text { get; set; } = "";
    [Parameter]
    public float FontSize { get; set; } = 12; //default at 12.  can be whatever i need.  for game of life, wants to use defaults if possible.
    protected override bool ShouldRender()
    {
        return false;
    }
}