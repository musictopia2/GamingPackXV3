namespace AggravationCP.Data;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<BasicList<BasicList<SimpleDice>>>()
            .Make<EnumColorChoice>();
    }
}