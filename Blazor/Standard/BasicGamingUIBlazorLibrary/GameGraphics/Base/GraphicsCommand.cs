using System.Reflection;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Base;
public abstract class GraphicsCommand : KeyComponentBase, IDisposable
{
    private bool _disposedValue;
    //related to the game button.  but this time only focus on the command part.
    
    /// <summary>
    /// this is the view model being used.  can be main view model, deck, etc.  this is not the card, etc.
    /// </summary>
    [Parameter]
    public object? CommandParameter { get; set; }
    [Parameter]
    public Action? AfterChange { get; set; }
    protected Assembly GetAssembly => Assembly.GetAssembly(GetType())!; //needs reflection namespace for the images.  decided to not attempt to string based on problems i ran across.
    [Parameter]
    public ICustomCommand? CommandObject { get; set; } //you always need this now.
    private CommandContainer? _commandContainer;
    private void RunProcess()
    {
        if (AfterChange is not null)
        {
            AfterChange.Invoke();
            return;
        }
        StateHasChanged();
    }
    protected override void OnInitialized()
    {
        if (CommandObject is not null)
        {
            _commandContainer = aa.Resolver!.Resolve<CommandContainer>();
            CommandObject.UpdateBlazor = RunProcess;
            if (CommandObject is IGameCommand game)
            {
                _commandContainer.AddCommand(game, RunProcess);
            }
        }
        base.OnInitialized();
    }
    public GraphicsCommand()
    {
        CustomCanDo = PrivateTo;
    }
    private bool PrivateTo() => CanProcess;
    public Func<bool> CustomCanDo { get; set; }
    public bool CanProcess
    {
        get
        {
            if (CommandObject == null)
            {
                return false;
            }
            return CommandObject.CanExecute(CommandParameter);
        }
    }
    public void CreateClick(ISvg svg) //needs to be entire svg
    {
        if (CommandObject != null) //don't always need the datacontext anymore.
        {
            svg.EventData.ActionClicked = Clicked;
        }
    }
    private async Task Clicked(object args1, object args2)
    {
        await Submit();
    }
    //needs to be protected so i can use razor syntax to override and would still work.
    protected virtual async Task Submit() //anything may need to do this.
    {
        try
        {
            if (CommandObject == null)
            {
                return; //nothing to submit
            }
            if (CommandObject.CanExecute(CommandParameter) == false)
            {
                return;
            }
            await CommandObject.ExecuteAsync(CommandParameter);
        }
        catch (Exception ex)
        {
            await BasicBlazorLibrary.Helpers.BlazorUIHelpers.MessageBox!.ShowMessageAsync($"There was an error.  The error was {ex.Message}.  Stack Trace Was {ex.StackTrace}");
        }
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                if (_commandContainer is null)
                {
                    return; //because there was no commandobject sent
                }
                if (CommandObject is IGameCommand other)
                {
                    _commandContainer!.RemoveCommand(other, RunProcess);
                }
                else
                {
                    _commandContainer!.RemoveAction(RunProcess);
                }
            }
            _disposedValue = true;
        }
    }
    public virtual void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
