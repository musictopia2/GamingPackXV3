namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
internal static class YahtzeeGlobalContainer<D>
    where D : SimpleDice, new()
{
    public static YahtzeeScoresheetViewModel<D>? GlobalSheet { get; set; }
}
