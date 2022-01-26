namespace BasicGamingUIBlazorLibrary.Shells;
public partial class SinglePlayerParentShell
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    public BasicData BasicData { get; set; }
    public IGameInfo GameInfo { get; set; }
    public SinglePlayerParentShell()
    {
        BasicData = aa.Resolver!.Resolve<BasicData>();
        GameInfo = aa.Resolver!.Resolve<IGameInfo>();
    }
    protected override void OnInitialized()
    {
        IStartUp starts = aa.Resolver!.Resolve<IStartUp>();
        starts.StartVariables(BasicData);
        BasicData.ChangeState = ShowChange;
        base.OnInitialized();
    }
    private async void ShowChange()
    {
        await InvokeAsync(StateHasChanged);
    }
}