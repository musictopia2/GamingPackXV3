namespace SpellingBlazorLibrary;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<BasicList<WordInfo>>();
    }
}