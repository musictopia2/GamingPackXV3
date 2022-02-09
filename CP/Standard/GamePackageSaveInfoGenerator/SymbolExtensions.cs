namespace GamePackageSaveInfoGenerator;
internal static class SymbolExtensions
{
    public static string GetSubName(this IPropertySymbol pp)
    {
        EnumListCategory cat = pp.GetListCategory();
        //we may need simple type eventually (?)
        //EnumTypeCategory type = pp.Type.GetSimpleCategory();
        string name = pp.Type.Name;
        if (cat == EnumListCategory.None)
        {
            //this is the easiest.  this can change eventually
            return name;
        }
        var temps = pp.Type.GetSingleGenericTypeUsed()!.Name;

        if (cat == EnumListCategory.Single)
        {
            return $"{name}{temps}";
        }
        if (cat == EnumListCategory.Double)
        {
            return $"{name}{name}{temps}";
        }
        throw new Exception($"Nothing found for GetSubName.  The type name was {name} and the property name was {pp.Name}");
    }
    public static ITypeSymbol GetUnderlyingSymbol(this IPropertySymbol pp, EnumListCategory category, bool nullable)
    {
        if (category == EnumListCategory.None)
        {
            if (nullable == false)
            {
                return pp.Type;
            }
            return pp.Type.GetSingleGenericTypeUsed()!;
        }
        ITypeSymbol others;
        others = pp.GetSingleGenericTypeUsed()!;
        if (category == EnumListCategory.Single)
        {
            return others;
        }
        ITypeSymbol gg = others.GetSingleGenericTypeUsed()!;
        return gg;
    }
    public static ITypeSymbol GetUnderlyingSymbol(this IParameterSymbol pp, EnumListCategory category, bool nullable)
    {
        if (category == EnumListCategory.None)
        {
            if (nullable == false)
            {
                return pp.Type;
            }
            return pp.Type.GetSingleGenericTypeUsed()!;
        }
        ITypeSymbol others;
        others = pp.Type.GetSingleGenericTypeUsed()!;
        if (category == EnumListCategory.Single)
        {
            return others;
        }
        ITypeSymbol gg = others.GetSingleGenericTypeUsed()!;
        return gg;
    }
    public static EnumListCategory GetListCategory(this IParameterSymbol pp)
    {
        if (pp.Type.IsCollection() == false)
        {
            return EnumListCategory.None;
        }
        var others = pp.Type.GetSingleGenericTypeUsed();
        return others!.IsCollection() ? EnumListCategory.Double : EnumListCategory.Single;
    }
    public static EnumListCategory GetListCategory(this IPropertySymbol pp)
    {
        if (pp.IsCollection() == false)
        {
            if (pp.Type.Implements("IPlayerCollection") || pp.Type.Implements("ISimpleList"))
            {
                return EnumListCategory.Single; //for now, always single.  if double is needed, requires rethinking.
            }
            return EnumListCategory.None;
        }
        var others = pp.GetSingleGenericTypeUsed()!;
        return others.IsCollection() ? EnumListCategory.Double : EnumListCategory.Single;
    }
    public static EnumTypeCategory GetSimpleCategory(this ITypeSymbol symbol)
    {
        if (symbol.TypeKind == TypeKind.Enum)
        {
            return EnumTypeCategory.StandardEnum;
        }
        if (symbol.Name.StartsWith("Enum"))
        {
            return EnumTypeCategory.CustomEnum;
        }
        if (symbol.Name == "Int32")
        {
            return EnumTypeCategory.Int;
        }
        if (symbol.Name == "Boolean")
        {
            return EnumTypeCategory.Bool;
        }
        if (symbol.Name == "String")
        {
            return EnumTypeCategory.String;
        }
        if (symbol.Name == "Decimal")
        {
            return EnumTypeCategory.Decimal;
        }
        if (symbol.Name == "PointF")
        {
            return EnumTypeCategory.PointF;
        }
        if (symbol.Name == "SizeF")
        {
            return EnumTypeCategory.SizeF;
        }
        if (symbol.Name == "Vector")
        {
            return EnumTypeCategory.Vector;
        }
        return EnumTypeCategory.Complex;
    }
    public static bool PropertyIgnored(this IPropertySymbol p, BasicList<IPropertySymbol> completeIgnores)
    {
        foreach (var aa in completeIgnores)
        {
            if (aa.Name == p.Name && aa.OriginalDefinition.ToDisplayString() == p.OriginalDefinition.ToDisplayString())
            {
                return true;
            }
        }
        return false;
    }
}