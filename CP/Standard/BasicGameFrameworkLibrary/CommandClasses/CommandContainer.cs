namespace BasicGameFrameworkLibrary.CommandClasses;
public class CommandContainer
{
    private readonly BasicList<IGameCommand> _commandList = new();
    private readonly BasicList<IGameCommand> _openList = new();
    public event ExecutingChangedEventHandler? ExecutingChanged; //don't need anything else because can call a method to see where it stands.
    public delegate void ExecutingChangedEventHandler(); //i think
    private readonly BasicList<IControlObservable> _controlList = new();
    private readonly BasicList<Action> _allActions = new(); //this is not static though like the other one.
    private readonly Dictionary<string, Action> _specialActions = new();
    public Action? ParentAction { get; set; } //not sure if i can attempt where it focuses the parent.  if so, then can refactor.
    public CommandContainer()
    {
        IsExecuting = true; //i think it needs to start out as true.
    }
    public void UpdateAll() //this will update all no matter what.
    {
        if (ParentAction is not null)
        {
            //if there is a master parent, try to do this way.
            ParentAction.Invoke();
            return; //try this way (?)  this means no need to do anything else i think because the parent will make sure everything else updates properly.
        }
        var list = _allActions.ToBasicList();
        list.ForEach(a =>
        {
            if (a != null)
            {
                a.Invoke();
            }
        });
    }
    public void UpdateSpecificAction(string key)
    {
        if (_specialActions.ContainsKey(key) == false)
        {
            return; //because there is nothing.
        }
        _specialActions[key].Invoke();
    }
    public bool CanExecuteManually()
    {
        return !IsExecuting;
    }
    public void StartExecuting() //this goes to the update actions.
    {
        if (IsExecuting)
        {
            Processing = true; //try this way.  this is processing though.
            return; //try to ignore because its already executing.  this will prevent this other bug.
        } //try to not have any warnings anymore
        IsExecuting = true;
        Processing = true; //extra toasts are to help with debugging when you can't even log to console.
    }
    public async Task ProcessCustomCommandAsync<T>(Func<T, Task> action, T argument)
    {
        StartExecuting();
        await action.Invoke(argument);
        StopExecuting();
    }
    public async Task ProcessCustomCommandAsync(Func<Task> action)
    {
        StartExecuting();
        await action.Invoke();
        StopExecuting();
    }
    public void StopExecuting()
    {
        if (ManuelFinish == false)
        {
            IsExecuting = false;
        }
        Processing = false;
    }
    public void ClearLists()
    {
        _commandList.Clear();
        _openList.Clear();
        _controlList.Clear();
        _allActions.Clear(); //i think
        //hopefully the parent will be okay.  if the parent never goes away, maybe this will work.
    }
    private bool _executing;
    /// <summary>
    /// This is used when its not even your turn.
    /// Use Processing if you are able to do things out of turn as long
    /// as the other variable is false.
    /// </summary>
    /// <remarks>This is used when its not even your turn.
    /// Use Processing if you are able to do things out of turn as long
    /// as the other variable is false.</remarks>
    public bool IsExecuting
    {
        get
        {
            return _executing; //eventually have to change once i have multiplayer processes completed.
        }
        set
        {
            //eventually need more once i get the multiplayer processes completed.
            if (value == _executing)
            {
                return;
            }
            _executing = value;
            ReportAll();
        }
    }
    private bool _openBusy;
    public bool OpenBusy
    {
        get
        {
            return _openBusy;
        }
        set
        {
            if (value == _openBusy)
            {
                return;
            }
            _openBusy = value;
            ReportOpen();
        }
    }
    public void ReportOpen()
    {
        _openList.ForEach(x => x.ReportCanExecuteChange());
        UpdateAll();
    }
    /// <summary>
    /// the purpose of this one is if a move is in progress, then even if being rejoined, then has to wait until move is not in progress anymore.
    /// </summary>

    private bool _processing = true; //you need to start out that its processing.
    public bool Processing
    {
        get { return _processing; }
        set
        {
            if (value == _processing)
            {
                return;
            }
            _processing = value;
            ReportLimited();
        }
    }
    public void ReportLimited()
    {
        ReportItems(EnumCommandBusyCategory.Limited);
        UpdateAll();
    }
    public bool ManuelFinish { get; set; } = false;
    public void ManualReport()
    {
        _commandList.ForEach(x => x.ReportCanExecuteChange());
        _controlList.ForEach(x => x.ReportCanExecuteChange());
    }
    public void RemoveOldItems(object payLoad)
    {
        _commandList.RemoveAllOnly(x => x.Context == payLoad);
    }
    public void ReportAll() //when changing, will report to all no matter what.  decided it can be good to notify all that something has changed.
    {
        ReportItems(EnumCommandBusyCategory.None);
        if (ExecutingChanged != null)
        {
            ExecutingChanged.Invoke();
        }

        UpdateAll();
    }
    private void ReportItems(EnumCommandBusyCategory thisBusy)
    {
        _commandList.ForConditionalItems(items => items.BusyCategory == thisBusy
        , items => items.ReportCanExecuteChange());
        _controlList.ForConditionalItems(items => items.BusyCategory == thisBusy
        , items => items.ReportCanExecuteChange());
    }
    public void AddOpen(IGameCommand thisOpen, Action action)
    {
        _openList.Add(thisOpen);
        AddAction(action);
    }
    public void AddCommand(IGameCommand thisCommand, Action action)
    {
        _commandList.Add(thisCommand);
        AddAction(action);
    }
    public void AddCommand(IGameCommand command)
    {
        _commandList.Add(command);
    }
    public void AddControl(IControlObservable thisControl, Action action)
    {
        AddControl(thisControl);
        AddAction(action);
    }
    public void AddControl(IControlObservable thisControl)
    {
        _controlList.Add(thisControl);
    }
    public void AddAction(Action action)
    {
        if (_allActions.Contains(action) == false)
        {
            _allActions.Add(action);
        } //only if debugging do i need the else.
    }
    public void AddAction(Action action, string key)
    {
        if (_specialActions.ContainsKey(key) == false)
        {
            _specialActions.Add(key, action);
        }
    }
    public void RemoveCommand(IGameCommand command, Action action)
    {
        _commandList.RemoveSpecificItem(command);
        RemoveAction(action);
    }
    public void RemoveAction(Action action)
    {
        _allActions.RemoveSpecificItem(action);
    }
    public void RemoveAction(string key)
    {
        if (_specialActions.ContainsKey(key) == false)
        {
            return;
        }
        _specialActions.Remove(key);
    }
}