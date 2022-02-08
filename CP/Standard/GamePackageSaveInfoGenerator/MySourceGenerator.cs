using System.Diagnostics;

namespace GamePackageSaveInfoGenerator;
[Generator]
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
        return ctx.IsPublic();
    }
    private ClassDeclarationSyntax? GetTarget(GeneratorSyntaxContext context)
    {
        var ourClass = context.GetClassNode();
        var symbol = context.GetClassSymbol(ourClass);
        if (symbol.Implements("ISaveInfo"))
        {
            return ourClass;
        }
        return null;
    }
    private void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> list, SourceProductionContext context)
    {
        try
        {
            var others = list.Distinct();
            if (others.Count() == 0)
            {
                return;
            }
            if (others.Count() > 1)
            {
                context.
            }


        }
        catch (Exception ex)
        {
            context.AddSource("errors.g", $"//{ex.Message}");
        }
    }
}