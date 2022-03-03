namespace FluxxCP.Containers;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<BasicList<string>>()
            .Make<BasicList<PointF>>(); //if anything else, put here,
    }
}