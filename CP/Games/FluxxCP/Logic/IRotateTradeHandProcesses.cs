namespace FluxxCP.Logic;
public interface IRotateTradeHandProcesses
{
    Task RotateHandAsync(EnumDirection direction);
    Task TradeHandAsync(int selectedIndex);
}