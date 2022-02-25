namespace DeadDie96CP.Data;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<BasicList<BasicList<TenSidedDice>>>();
    }
}