namespace CommandsGenerator;
internal static class ExtraExtensions
{
    public static BasicList<ISymbol> GetCompleteCanList(this INamedTypeSymbol symbol)
    {
        var firsts = symbol.GetProperties();
        BasicList<ISymbol> output = new();
        foreach (var item in firsts)
        {
            if (item.Name.StartsWith("Can") && item.DeclaredAccessibility == Accessibility.Public)
            {
                output.Add(item);
            }
        }
        var nexts = symbol.GetMembers().OfType<IMethodSymbol>();
        foreach (var item in nexts)
        {
            if (item.Name.StartsWith("Can") && item.DeclaredAccessibility == Accessibility.Public)
            {
                output.Add(item);
            }
        }
        return output;
    }
    public static ISymbol? GetCanSymbol(this IMethodSymbol lookup, BasicList<ISymbol> list)
    {
        string name;
        name = lookup.Name.Replace("Async", "");
        name = $"Can{name}";
        foreach (var item in list)
        {
            if (item.Name == name)
            {
                return item; //the first one is fine.
            }
        }
        return null;
    }
    //also too specialized for putting to globals.
    public static string GetEnumText(this EnumCommandCategory category)
    {
        if (category == EnumCommandCategory.Control)
        {
            return "Control";
        }
        if (category == EnumCommandCategory.Game)
        {
            return "Game";
        }
        if (category == EnumCommandCategory.Limited)
        {
            return "Limited";
        }
        if (category == EnumCommandCategory.Old)
        {
            return "Old";
        }
        if (category == EnumCommandCategory.Open)
        {
            return "Open";
        }
        if (category == EnumCommandCategory.OutOfTurn)
        {
            return "Out Of Turn";
        }
        if (category == EnumCommandCategory.Plain)
        {
            return "Plain";
        }
        return "Unknown";
    }
    public static IWriter AppendCommandType(this IWriter w, EnumCommandCategory category)
    {
        w.GlobalWrite()
            .Write("BasicGameFrameworkLibrary.CommandClasses.");
        if (category == EnumCommandCategory.Open)
        {
            w.Write("OpenCommand");
        }
        if (category == EnumCommandCategory.Plain)
        {
            w.Write("PlainCommand");
        }
        if (category == EnumCommandCategory.Old)
        {
            w.Write("OldCommand");
        }
        if (category == EnumCommandCategory.OutOfTurn)
        {
            w.Write("OutOfTurnCommand");
        }
        if (category == EnumCommandCategory.Limited)
        {
            w.Write("LimitedCommand");
        }
        if (category == EnumCommandCategory.Control)
        {
            w.Write("ControlCommand");
        }
        if (category == EnumCommandCategory.Game)
        {
            w.Write("BasicGameCommand");
        }
        w.Write("? ");
        return w;
    }
    public static IWriter AppendCommandName(this IWriter w, CommandInfo info)
    {
        w.Write(info.MethodName)
            .Write("Command");
        return w;
    }
    public static IWriter StartNewCommandMethod(this IWriter w)
    {
        w.Write(" = new(this, ");
        return w;
    }
    public static IWriter EndNewCommandMethod(this IWriter w)
    {
        w.Write(");");
        return w;
    }
    public static IWriter PopulateRestCommand(this IWriter w, CommandInfo info)
    {
        if (info.CanSymbol is null)
        {
            if (info.ParameterUsed is null)
            {
                if (info.IsAsync)
                {
                    w.WriteAsyncMethod1(info);
                    return w;
                }
                w.WriteRegularMethod1(info);
                return w;
            }
            if (info.IsAsync)
            {
                w.WriteAsyncMethod2(info);
            }
            else
            {
                w.WriteRegularMethod2(info);
            }
            return w;

        }
        if (info.ParameterUsed is null)
        {
            if (info.IsAsync)
            {
                w.WriteAsyncMethod1(info);
            }
            else
            {
                w.WriteRegularMethod1(info);
            }
            w.Write(", ");
            w.WriteRegularCan1(info);
            return w;
        }
        if (info.IsAsync)
        {
            w.WriteAsyncMethod2(info);
        }
        else
        {
            w.WriteRegularMethod2(info);
        }
        w.Write(", ");
        w.WriteRegularCan2(info);
        return w;
    }
    private static void WriteRegularMethod1(this IWriter w, CommandInfo info)
    {
        w.Write("simpleAction1: ")
            .Write(info.MethodSymbol!.Name);
    }
    private static void WriteAsyncMethod1(this IWriter w, CommandInfo info)
    {
        w.Write("simpleAsync1: ")
            .Write(info.MethodSymbol!.Name);
    }
    private static void WriteRegularCan1(this IWriter w, CommandInfo info)
    {
        if (info.CanSymbol is IPropertySymbol)
        {
            w.Write("canExecute1: () => ");
        }
        else
        {
            w.Write("canExecute1: ");
        }
        w.Write(info.CanSymbol!.Name);
    }
    private static void WriteRegularMethod2(this IWriter w, CommandInfo info)
    {
        w.Write("simpleAction2: (value) => ")
            .Write(info.MethodSymbol!.Name)
            .WriteCast(info);
    }
    private static void WriteAsyncMethod2(this IWriter w, CommandInfo info)
    {
        w.Write("simpleAsync2: (value) => ")
            .Write(info.MethodSymbol!.Name)
            .WriteCast(info);
    }
    private static void WriteRegularCan2(this IWriter w, CommandInfo info)
    {
        w.Write("canExecute2: (value) => ")
            .Write(info.CanSymbol!.Name)
            .WriteCast(info);
    }
    private static void WriteCast(this IWriter w, CommandInfo info)
    {
        w.Write("((")
            .GlobalWrite()
            .Write(info.ParameterUsed!.ContainingNamespace.ToDisplayString())
            .Write(".")
            .Write(info.ParameterUsed!.Name)
            .Write(")")
            .Write(" value!)");
    }
}