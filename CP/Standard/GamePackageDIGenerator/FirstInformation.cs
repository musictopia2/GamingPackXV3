namespace GamePackageDIGenerator;
internal class FirstInformation
{
    public INamedTypeSymbol? MainClass { get; set; }
    public BasicList<INamedTypeSymbol> Constructors { get; set; } = new();
    public BasicList<INamedTypeSymbol> Assignments { get; set; } = new();
    public EnumCategory Category { get; set; }
    public string Tag { get; set; } = "";

}