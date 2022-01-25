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
        INamedTypeSymbol? container = _compilation.GetTypeByMetadataName("BasicGameFrameworkLibrary.DIContainers.IGamePackageRegister"); //this means that interfaces won't capture the methods.  try the real implementation (hopefully okay when somebody chooses a different way of doing it)
        BasicList<FirstInformation> output = new();
        //output.ContainerSymbol = container;
        var firstTemp = container!.GetAllPublicMethods();
        BasicList<string> generics = new();
        BasicList<string> regulars = new();
        //string regular = "";
        string instance = "";
        foreach (var item in firstTemp)
        {
            if (item.Name == "RegisterSingleton")
            {
                if (item.TypeArguments.Count() == 2)
                {
                    generics.Add(item.ToString());
                }
                else
                {
                    regulars.Add(item.ToString());
                    //regular = item.ToString();
                }
            }
            else if (item.Name == "RegisterInstanceType")
            {
                instance = item.ToString();
            }
            else if (item.Name == "RegisterType")
            {
                generics.Add(item.ToString()); //try another possibility for generics.
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
                        bool isGeneric = false;
                        bool isRegular = false;
                        //isRegular = results == regular || results == instance;
                        foreach (var r in regulars)
                        {
                            if (results == r)
                            {
                                isRegular = true;
                            }
                        }
                        if (isRegular == false && results == instance)
                        {
                            isRegular = true;
                        }
                        foreach (var g in generics)
                        {
                            if (results == g)
                            {
                                isGeneric = true;
                                break;
                            }
                        }
                        if (isGeneric == false && isRegular == false)
                        {
                            continue;
                        }
                        FirstInformation fins = new();
                        var possibleTag = expressPossible.DescendantNodes().OfType<LiteralExpressionSyntax>().SingleOrDefault();
                        if (possibleTag is not null)
                        {
                            fins.Tag = possibleTag.Token.ValueText;
                        }
                        if (isGeneric)
                        {
                            var i = expressPossible.DescendantNodes().OfType<IdentifierNameSyntax>().ToBasicList();
                            INamedTypeSymbol lasts = (INamedTypeSymbol)model.GetSymbolInfo(i.Last()).Symbol!;
                            fins.MainClass = lasts;
                            output.Add(fins);
                        }
                        else if (isRegular)
                        {

                            var l = expressPossible.DescendantNodes().OfType<IdentifierNameSyntax>().Last();
                            INamedTypeSymbol symbol = (INamedTypeSymbol)model.GetSymbolInfo(l).Symbol!;
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