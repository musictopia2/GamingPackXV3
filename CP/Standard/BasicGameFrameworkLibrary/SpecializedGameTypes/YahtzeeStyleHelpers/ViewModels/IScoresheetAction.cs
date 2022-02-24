namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels;
public interface IScoresheetAction
{
    Task RowAsync(RowInfo row);
}