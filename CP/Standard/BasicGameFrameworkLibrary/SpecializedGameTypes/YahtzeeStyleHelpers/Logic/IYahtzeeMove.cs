namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic;
public interface IYahtzeeMove
{
    Task MakeMoveAsync(RowInfo row);
}