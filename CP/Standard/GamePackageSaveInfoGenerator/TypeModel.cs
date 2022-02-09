namespace GamePackageSaveInfoGenerator;
internal class TypeModel
{
    public string GetGlobalNameSpace => $"global::{SymbolUsed!.ContainingNamespace.ToDisplayString()}";
    public string CollectionNameSpace { get; set; } = "";
    public string CollectionStringName { get; set; } = "";
    public bool NullablePossible { get; set; } = false; //try to require to be true (?)
    public string FileName { get; set; } = ""; //try to search by filename now.
    public string SubName { get; set; } = ""; //if its generic, then needs to get the name of the underlying one.
    public INamedTypeSymbol? SubSymbol { get; set; }
    public ITypeSymbol? SymbolUsed { get; set; }

    //for now, we chosed SubSymbol.
    //if that idea works, great.
    //if not, rethink.


    /// <summary>
    /// this will be when there are generics but is not the custom lists though.
    /// </summary>
   // public BasicList<ITypeSymbol> GenericsUsed { get; set; } = new();
    public string TypeName => SymbolUsed!.Name;
    public EnumLoopCategory LoopCategory { get; set; }
    public EnumListCategory ListCategory { get; set; }
    public EnumTypeCategory TypeCategory { get; set; }
    public EnumSpecialCategory SpecialCategory { get; set; }
    public BasicList<string> EnumNames { get; set; } = new(); //for enums, can capture it here for efficiency (instead of running several times)
    //public bool IsIDeck { get; set; } //anything that implements IDeck requires special treatment.
}