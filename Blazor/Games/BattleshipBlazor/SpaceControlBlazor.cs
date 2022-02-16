namespace BattleshipBlazor;
public class SpaceControlBlazor : GraphicsCommand
{
    [Parameter]
    public FieldInfoCP? Field { get; set; }
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Field == null)
        {
            return;
        }
        string color = Field.FillColor.ToWebColor();
        ISvg svg = new SVG();
        SvgRenderClass render = new();
        Rect rect = new();
        rect.Width = "50";
        rect.Height = "50";
        rect.Fill = color;
        rect.PopulateStrokesToStyles(strokeWidth: 4);
        svg.Children.Add(rect);
        if (Field.Hit == EnumWhatHit.Hit)
        {
            Image image = new();
            image.Width = "50";
            image.Height = "50";
            image.PopulateFullExternalImage(this, "battleshipfire.svg");
            svg.Children.Add(image);
        }
        CreateClick(svg);
        render.RenderSvgTree(svg, 0, builder);
        base.BuildRenderTree(builder);
    }
}