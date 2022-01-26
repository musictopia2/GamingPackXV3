using BasicBlazorLibrary.Components.MediaQueries.ParentClasses; //not common enough.
namespace BasicGamingUIBlazorLibrary.StartupClasses;
public partial class MainPage
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public bool FromLayout { get; set; }
    [CascadingParameter]
    private MediaQueryListComponent? MediaQuery { get; set; }
    private string Height => $"{MediaQuery!.BrowserInfo!.Height}px";
}