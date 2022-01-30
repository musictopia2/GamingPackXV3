namespace CommandsGenerator;
internal class CommandInfo
{
    public ISymbol? CanSymbol { get; set; }
    public EnumCommandCategory Category { get; set; }
    public IMethodSymbol? MethodSymbol { get; set; }
    public bool IsAsync { get; set; }
    public INamedTypeSymbol? ParameterUsed { get; set; }
    public bool IsProperty { get; set; }
    public bool NotImplemented { get; set; }
    public bool InvalidCast { get; set; }
    public bool WrongReturnType { get; set; }
    public EnumMiscCategory MiscError { get; set; } = EnumMiscCategory.None;
    public bool ReportedError { get; set; }
    public EnumCreateCategory CreateCategory { get; set; }
    public string MethodName { get; set; } = "";
}