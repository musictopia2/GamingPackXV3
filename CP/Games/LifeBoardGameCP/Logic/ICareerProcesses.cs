namespace LifeBoardGameCP.Logic;
public interface ICareerProcesses
{
    Task ChoseCareerAsync(int career);
    void LoadCareerList();
}