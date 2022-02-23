namespace LifeBoardGameCP.Logic;
public interface ITwinProcesses
{
    Task GetTwinsAsync(BasicList<EnumGender> twinList);
}