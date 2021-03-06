namespace RiskBlazor;
public class ArmyColorComponent : ComponentBase
{
    [CascadingParameter]
    public BasePieceGraphics? MainGraphics { get; set; }
    [Parameter]
    public string MainColor { get; set; } = cc.Transparent;
    protected override void OnInitialized()
    {
        MainGraphics!.OriginalSize = new SizeF(2000, 2000);
        MainGraphics.BorderWidth = 1;
        MainGraphics.HighlightTransparent = true; //i think.
        base.OnInitialized();
    }
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        ISvg svg = MainGraphics!.GetMainSvg(false);
        svg.DrawArmyPiece(MainColor);
        SvgRenderClass render = new();
        render.RenderSvgTree(svg, 0, builder);
        base.BuildRenderTree(builder);
    }
}