namespace BattleshipLiteBlazor;
public class SpaceControlBlazor : GraphicsCommand
{
    [Parameter]
    public ShipInfo? Ship { get; set; }
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Ship == null)
        {
            return;
        }
        string color = Ship.FillColor().ToWebColor();
        ISvg svg = new SVG();
        SvgRenderClass render = new();
        Rect rect = new();
        rect.Width = "50";
        rect.Height = "50";
        rect.Fill = color;
        rect.PopulateStrokesToStyles(strokeWidth: 4);
        svg.Children.Add(rect);
        if (Ship.ShipStatus == EnumShipStatus.Hit)
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