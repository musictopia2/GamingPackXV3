namespace SinisterSixCP.Data;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<BasicList<BasicList<EightSidedDice>>>();
    }
}