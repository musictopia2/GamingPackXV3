namespace CommandsGenerator;
internal class CompleteInfo
{
    public INamedTypeSymbol? ClassSymbol { get; set; }
    public BasicList<CommandInfo> Commands { get; set; } = new();
    public bool HasPartialCreateCommandsOnly { get; set; }
    public string ContainerName { get; set; } = "";
    public bool NeedsCommandsOnly { get; set; }
    public bool NeedsCommandContainer { get; set; }
    public bool HasPartialClass { get; set; }
}