namespace LifeBoardGameCP.Logic;
public interface IChooseStockProcesses
{
    Task ChoseStockAsync(int stock);
    void LoadStockList();
}