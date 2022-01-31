namespace BasicGamingUIBlazorLibrary.GameGraphics.Cards;
public abstract class BaseDarkCardsBlazor<D> : BaseDeckGraphics<D>
    where D : class, IDeckObject, new()
{
    protected override string SelectFillColor => cc.Black.ToWebColor();
    protected override string DrawFillColor => cc.White.ToWebColor();
    protected abstract bool IsLightColored { get; }
    protected override string GetOpacity
    {
        get
        {
            if (IsLightColored && DeckObject!.IsSelected == false)
            {
                return ".75";
            }
            return GetDarkHighlighter().ToString();
        }
    }
}