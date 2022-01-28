namespace BasicGamingUIBlazorLibrary.GameGraphics.Base;
public partial class BorderedCommandSpace
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public int BorderWidth { get; set; }
    [Parameter]
    public string BorderColor { get; set; } = ""; //if no bordercolor, then don't even use this.
    [Parameter]
    public string FillColor { get; set; } = cc.Transparent;
    private string ShapeStyle()
    {
        string output = $"stroke: {BorderColor.ToWebColor()}; stroke-width: {BorderWidth}; stroke-miterlimit:4;";
        return output;
    }
}