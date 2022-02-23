namespace LifeBoardGameCP.Logic;
public interface IReturnStockProcesses
{
    Task StockReturnedAsync(int stock);
    void LoadCurrentPlayerStocks();
}