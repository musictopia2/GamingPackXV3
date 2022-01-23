namespace BasicGameFrameworkLibrary.CommandClasses;
public class PlainCommand : ParentCommand, IGameCommand
{
    //source generators generate this.
    public PlainCommand(object model,
                        CommandContainer command,
                        Func<Task>? simpleAsync1 = null,
                        Func<object?, Task>? simpleAsync2 = null,
                        Action? simpleAction1 = null,
                        Action<object?>? simpleAction2 = null,
                        Func<bool>? canExecute1 = null,
                        Func<object?, bool>? canExecute2 = null,
                        string functionName = "") : base(model, simpleAsync1, simpleAsync2, simpleAction1, simpleAction2, canExecute1, canExecute2, functionName)
    {
        CommandContainer = command;
        HookUpNotifiers();
    }
    public EnumCommandBusyCategory BusyCategory { get; set; }
    protected CommandContainer CommandContainer { get; set; }
    //try to not do anything for updating.  does mean that the game button needs the command container (could be nullable).


    protected virtual void StartExecuting()
    {
        CommandContainer.StartExecuting();
    }
    protected virtual void StopExecuting()
    {
        CommandContainer.StopExecuting();
    }
    public virtual bool CanExecute(object? parameter)
    {
        if (InProgressHelpers.MoveInProgress)
        {
            return false;
        }
        //has to eventually figure out another delegate from multiplayer (?)
        if (CommandContainer.IsExecuting == true && BusyCategory == EnumCommandBusyCategory.None)
        {
            return false;
        }
        if (CommandContainer.Processing == true && BusyCategory == EnumCommandBusyCategory.Limited)
        {
            return false;
        }
        return ParentCanExecute(parameter);
    }
    public override async Task ExecuteAsync(object? parameter)
    {
        if (CanExecute(parameter) == false)
        {
            return;
        }
        StartExecuting();
        await base.ExecuteAsync(parameter);
        StopExecuting();
    }
}