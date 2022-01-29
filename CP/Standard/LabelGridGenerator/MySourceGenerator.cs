using System.Diagnostics;
namespace LabelGridGenerator;
[Generator] //this is important so it knows this class is a generator which will generate code for a class using it.
public class MySourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //#if DEBUG
        //        if (Debugger.IsAttached == false)
        //        {
        //            Debugger.Launch();
        //        }
        //#endif
        context.RegisterPostInitializationOutput(c => c.CreateCustomSource().AddAttributesToSourceOnly());
        IncrementalValuesProvider<ClassDeclarationSyntax> declares = context.SyntaxProvider.CreateSyntaxProvider(
            (s, _) => IsSyntaxTarget(s),
            (t, _) => GetTarget(t))
            .Where(m => m != null)!;
        IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilation
            = context.CompilationProvider.Combine(declares.Collect());
        context.RegisterSourceOutput(compilation, (spc, source) =>
        {
            Execute(source.Item1, source.Item2, spc);
        });
    }
    private bool IsSyntaxTarget(SyntaxNode syntax)
    {
        if (syntax is not ClassDeclarationSyntax ctx)
        {
            return false;
        }
        if (ctx.IsPublic() == false)
        {
            return false;
        }
        foreach (var item in ctx.Members)
        {
            if (item.AttributeLists.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
    private ClassDeclarationSyntax? GetTarget(GeneratorSyntaxContext context)
    {
        var ourClass = context.GetClassNode();
        var symbol = context.GetClassSymbol(ourClass);
        foreach (var item in symbol.GetMembers().OfType<IPropertySymbol>())
        {
            if (item.HasAttribute(aa.LabelGrid.LabelGridAttribute))
            {
                return ourClass;
            }
        }
        return null;
    }
    private void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> list, SourceProductionContext context)
    {
        //at this point, we have a list of classes.  its already been filtered.
        var others = list.Distinct();
        ParserClass parses = new(compilation);
        var results = parses.GetResults(others); //the parsing is now finished.  now has to do the rest.
        EmitClass emits = new(context, results);
        emits.Emit();
    }
}