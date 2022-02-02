namespace GamePackageDIGenerator;
internal static class ExtraExtensions
{
    //for now, just do as temporary.  not worth the risk of breaking existing source generators.  maybe for the next redo, i can incorporate this in.
    public static IWriter SymbolFullNameWrite(this IWriter w, INamedTypeSymbol symbol)
    {
        w.GlobalWrite()
            .Write(symbol.ContainingNamespace)
            .Write(".")
            .Write(symbol.Name)
            .Write(symbol.GetGenericString()!);
        return w;
    }
}