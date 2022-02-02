namespace GamePackageDIGenerator;
internal static class SymbolExtensions
{
    public static IWriter PopulateTypeOf(this IWriter w, INamedTypeSymbol symbol)
    {
        w.Write("typeof(")
                   .SymbolFullNameWrite(symbol)
                   .Write(")");
        return w;
    }
}