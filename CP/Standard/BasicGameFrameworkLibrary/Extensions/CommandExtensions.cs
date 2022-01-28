namespace BasicGameFrameworkLibrary.Extensions;
public static class CommandExtensions
{
    //public static BasicList<BoardCommand> GetBoardCommandList(this ISeveralCommands vm)
    //{
    //    BasicList<BoardCommand> output = new();
    //    Type type = vm.GetType();
    //    BasicList<MethodInfo> methods = type.GetMethods().ToBasicList(); //decided to just show all methods period.
    //    methods.ForEach(x =>
    //    {
    //        output.Add(new BoardCommand(vm, x, vm.BlazorAction, vm.Command, x.Name));
    //    });
    //    BoardCommand board = output.First();
    //    return output;
    //}
    //this is all that is needed for now.
    public static bool CanExecuteBasics(this CommandContainer command)
    {
        if (command.IsExecuting == true || command.Processing)
        {
            return false;
        }
        return true;
    }
    //public static PlainCommand GetPlainCommand(this ISeveralCommands payLoad, string name)
    //{
    //    MethodInfo? method = payLoad.GetPrivateMethod(name);
    //    if (method == null)
    //    {
    //        Type type = payLoad.GetType();
    //        throw new CustomBasicException($"Method with the name of {name} was not found  Type was {type.Name}");
    //    }
    //    PlainCommand output = new(payLoad, method, canExecuteM: null!, payLoad.BlazorAction, payLoad.Command);
    //    return output;
    //}
}
