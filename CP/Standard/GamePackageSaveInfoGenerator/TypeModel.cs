namespace GamePackageSaveInfoGenerator;
internal class TypeModel
{
    public string GetGlobalNameSpace => $"global::{SymbolUsed!.ContainingNamespace.ToDisplayString()}";
    public string CollectionNameSpace { get; set; } = "";
    public string CollectionStringName { get; set; } = "";
    public string FileName { get; set; } = ""; //try to search by filename now.
    public string SubName { get; set; } = ""; //if its generic, then needs to get the name of the underlying one.
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
    //public bool IsIDeck { get; set; } //anything that implements IDeck requires special treatment.
}