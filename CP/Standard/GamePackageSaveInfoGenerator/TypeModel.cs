namespace GamePackageSaveInfoGenerator;
internal class TypeModel
{
    public string GetGlobalNameSpace => $"global::{SymbolUsed!.ContainingNamespace.ToDisplayString()}";
    public string CollectionNameSpace { get; set; } = "";
    public string CollectionStringName { get; set; } = "";
    public bool NullablePossible { get; set; } = true; //if false, then will create new one for some functions.  has to be proven to be false.  only way it knows is from the property scan.
    public string FileName { get; set; } = ""; //try to search by filename now.
    public string SubName { get; set; } = ""; //if its generic, then needs to get the name of the underlying one.
    public INamedTypeSymbol? SubSymbol { get; set; }
    public ITypeSymbol? SymbolUsed { get; set; }
    /// <summary>
    /// this will be when there are generics but is not the custom lists though.
    /// </summary>
    public BasicList<ITypeSymbol> GenericsUsed { get; set; } = new();
    public string TypeName => SymbolUsed!.Name;
    public EnumLoopCategory LoopCategory { get; set; }
    public EnumListCategory ListCategory { get; set; }
    public EnumTypeCategory TypeCategory { get; set; }
    public EnumSpecialCategory SpecialCategory { get; set; }
    public BasicList<string> EnumNames { get; set; } = new(); //for enums, can capture it here for efficiency (instead of running several times)
    //public bool IsIDeck { get; set; } //anything that implements IDeck requires special treatment.
}