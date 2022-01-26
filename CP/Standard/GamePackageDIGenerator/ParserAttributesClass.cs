﻿namespace GamePackageDIGenerator;
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