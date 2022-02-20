using System.Text;

namespace GamePackageDIGenerator;
internal static class ExtraExtensions
{
    //for now, just do as temporary.  not worth the risk of breaking existing source generators.  maybe for the next redo, i can incorporate this in.
    //public static IWriter SymbolFullNameWrite(this IWriter w, INamedTypeSymbol symbol)
    //{
    //    w.GlobalWrite()
    //        .Write(symbol.ContainingNamespace)
    //        .Write(".")
    //        .Write(symbol.Name)
    //        .Write(symbol.GetGenericString()!);
    //    return w;
    //}
    public static IWriter SymbolFullNameWrite(this IWriter w, INamedTypeSymbol symbol, Dictionary<string, INamedTypeSymbol> matches)
    {
        //w.GlobalWrite()
        //    .Write(symbol.ContainingNamespace)
        //    .Write(".")
        //    .Write(symbol.Name)
        //    .Write(symbol.GetGenericString()!);
        //return w;


        w.GlobalWrite()
           .Write(symbol.ContainingNamespace)
           .Write(".")
           .Write(symbol.Name);
        if (matches.Count == 0)
        {
            w.Write(symbol.GetGenericString()!);
            
        }
        else
        {
            w.Write(symbol.GetGenericString(matches));
        }
        return w;
    }
    private static string GetGenericString(this INamedTypeSymbol symbol, Dictionary<string, INamedTypeSymbol> matches)
    {
        if (symbol.TypeArguments.Count() == 0)
        {
            return "";
        }
        StringBuilder builder = new();
        builder.Append("<");
        int index = 0;
        foreach (var item in symbol.TypeArguments)
        {
            if (index > 0)
            {
                builder.Append(", ");
            }
            if (item.TypeKind == TypeKind.TypeParameter)
            {
                var result = matches[item.Name];
                builder.Append("global::")
                    .Append(result.ContainingNamespace)
                    .Append(".")
                    .Append(result.Name);
            }
            else
            {
                builder.Append("global::")
                .Append(item.ContainingNamespace)
                .Append(".")
                .Append(item.Name);
            }
            index++;
        }
        builder.Append(">");
        return builder.ToString();
    }
}