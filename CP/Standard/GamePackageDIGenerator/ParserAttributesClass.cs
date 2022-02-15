namespace GamePackageDIGenerator;
internal class ParserAttributesClass
{
    private readonly Compilation _compilation;
    public ParserAttributesClass(Compilation compilation)
    {
        _compilation = compilation;
    }
    public BasicList<FirstInformation> GetResults(IEnumerable<ClassDeclarationSyntax> list)
    {
        BasicList<FirstInformation> output = new();
        foreach (var item in list)
        {
            FirstInformation firsts = new();

            SemanticModel model = _compilation.GetSemanticModel(item.SyntaxTree);
            firsts.MainClass = (INamedTypeSymbol)model.GetDeclaredSymbol(item)!;
            bool instance = firsts.MainClass.HasAttribute(aa.InstanceGame.InstanceGameAttribute);
            bool singleton = firsts.MainClass.HasAttribute(aa.SingletonGame.SingletonGameAttribute);
            if (instance && singleton)
            {
                firsts.Category = EnumCategory.Error; //can't do both.
            }
            else if (instance)
            {
                firsts.Category = EnumCategory.Instance;
            }
            else if (singleton)
            {
                firsts.Category = EnumCategory.Singleton;
            }
            else
            {
                firsts.Category = EnumCategory.None; //if none, just ignore.
            }
            if (firsts.Category != EnumCategory.None)
            {
                output.Add(firsts);
            }
        }
        //i think this may be still needed.
        foreach (var item in output)
        {
            var temps = item.MainClass!.AllInterfaces.ToBasicList();
            foreach (var temp in temps)
            {
                if (temp.Name != "IHandle" && temp.Name != "IHandleAsync")
                {
                    item.Assignments.Add(temp);
                } //cannot do anything with ihandle or ihandleasync since event aggravation handles that anyways.
            }
            //item.Assignments = item.MainClass!.AllInterfaces.ToBasicList();
            var tests = item.MainClass!.Constructors.OrderByDescending(x => x.Parameters.Count()).FirstOrDefault();
            var nexts = item.MainClass!.Constructors.OrderByDescending(x => x.Parameters.Count()).FirstOrDefault().Parameters.ToBasicList();
            foreach (var a in nexts)
            {
                var symbol = a.Type;
                item.Constructors.Add((INamedTypeSymbol)symbol);
            }
        }
        //i think i need to do a third part though.
        //BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses.PlayOrderClass
        //INamedTypeSymbol? container = _compilation.GetTypeByMetadataName("BasicGameFrameworkLibrary.DIContainers.IGamePackageRegister");

        INamedTypeSymbol? playSymbol = _compilation.GetTypeByMetadataName("BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses.PlayOrderClass");
        FirstInformation lasts = new();
        lasts.MainClass = playSymbol;
        lasts.Assignments = playSymbol!.Interfaces.ToBasicList(); //try allinterfaces.
        //no constructors on this one.
        lasts.Category = EnumCategory.None; //try this one.
        output.Add(lasts);

        return output;
    }
}