namespace XactikaCP.Logic;
[SingletonGame]
public class XactikaDelegates
{
    internal Func<Task>? LoadModeAsync { get; set; }
    internal Func<Task>? CloseModeAsync { get; set; }
}