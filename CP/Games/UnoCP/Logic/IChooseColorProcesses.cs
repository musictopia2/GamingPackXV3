namespace UnoCP.Logic;
public interface IChooseColorProcesses
{
    Task ColorChosenAsync(EnumColorTypes color);
}