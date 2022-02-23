namespace LifeBoardGameCP.Cards;
public class StockInfo : LifeBaseCard
{
    public StockInfo()
    {
        CardCategory = EnumCardCategory.Stock;
    }
    public int Value { get; set; }
}