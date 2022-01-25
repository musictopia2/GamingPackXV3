namespace GamePackageDIGenerator;
internal class ParserBasicClass
{
    private readonly Compilation _compilation;
    public ParserBasicClass(Compilation compilation)
    {
        _compilation = compilation;
    }
    public BasicList<FirstInformation> GetResults(IEnumerable<ClassDeclarationSyntax> list)
    {
        INamedTypeSymbol? container = _compilation.GetTypeByMetadataName("BasicGameFrameworkLibrary.DIContainers.IGamePackageRegister"); //still needed.  hopefully with interfaces is good enough (?)
        BasicList<FirstInformation> output = new();
        //output.ContainerSymbol = container;
        var firstTemp = container!.GetAllPublicMethods();
        string generic = "";
        string regular = "";
        foreach (var item in firstTemp)
        {
            if (item.Name == "RegisterSingleton")
            {
                if (item.TypeArguments.Count() == 2)
                {
                    generic = item.ToString();
                }
                else
                {
                    regular = item.ToString();
                }
            }
        }
        foreach (var item in list)
        {
            var model = _compilation.GetSemanticModel(item.SyntaxTree);
            foreach (var method in item.Members.OfType<MethodDeclarationSyntax>())
            {
                var expressList = method.DescendantNodes().OfType<ExpressionStatementSyntax>();
                foreach (var expressPossible in expressList)
                {
                    var needs = expressPossible.DescendantNodes().First();
                    if (model.GetSymbolInfo(needs).Symbol is IMethodSymbol t)
                    {
                        string results = t.ConstructedFrom.ToString();
                        if (results == generic)
                        {
                            var i = expressPossible.DescendantNodes().OfType<IdentifierNameSyntax>().ToBasicList();
                            INamedTypeSymbol lasts = (INamedTypeSymbol)model.GetSymbolInfo(i.Last()).Symbol!;
                            FirstInformation fins = new();
                            fins.MainClass = lasts;
                            output.Add(fins);

                        }
                        else if (results == regular)
                        {
                            var l = expressPossible.DescendantNodes().Last();
                            INamedTypeSymbol symbol = (INamedTypeSymbol)model.GetSymbolInfo(l).Symbol!;
                            FirstInformation fins = new();
                            fins.MainClass = symbol;
                            output.Add(fins);
                        }
                    }
                }
            }
        }
        foreach (var item in output)
        {
            item.Assignments = item.MainClass!.Interfaces.ToBasicList();
            var tests = item.MainClass!.Constructors.OrderByDescending(x => x.Parameters.Count()).FirstOrDefault();
            var nexts = item.MainClass!.Constructors.OrderByDescending(x => x.Parameters.Count()).FirstOrDefault().Parameters.ToBasicList();
            foreach (var a in nexts)
            {
                var symbol = a.Type;
                item.Constructors.Add((INamedTypeSymbol)symbol);
            }
        }
        return output;
    }
}