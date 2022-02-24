namespace KismetCP.Data;
internal class MainContext : SerializeContext
{
    protected override void Configure(ISerializeConfig config)
    {
        config.Make<BasicList<BasicList<KismetDice>>>()
            .Make<YahtzeeSaveInfo<KismetDice>>();
    }
}