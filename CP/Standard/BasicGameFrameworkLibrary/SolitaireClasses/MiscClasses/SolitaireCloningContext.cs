namespace BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses;
internal class SolitaireCloningContext : MainContext
{
    protected override void Configure(ICustomConfig config)
    {
        config.Make<SavedDiscardPile<SolitaireCard>>(x =>
        {
            x.Cloneable(false, x =>
            {

            });
        });
    }
}