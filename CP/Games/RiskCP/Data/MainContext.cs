namespace RiskCP.Data;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<EnumColorChoice>()
            .Make<Dictionary<string, string>>();
        //if there is a special type to serialize to send to other players that is not integers, then needs to do so.
        //if the type is int, then rethink.
    }
}