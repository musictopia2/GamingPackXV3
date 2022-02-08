namespace GamePackageSaveInfoGenerator;
internal class ParserClass
{
    private readonly Compilation _compilation;
    private HashSet<string> _properties = new();
    private HashSet<TypeModel> _types = new();
    private HashSet<string> _lookedAt = new();
    public ParserClass(Compilation compilation)
    {
        _compilation = compilation;
    }
    private void Clear()
    {
        _properties = new();
        _types = new();
        _lookedAt = new();
    }
    public CompleteInformation GetResults(ClassDeclarationSyntax node)
    {
        Clear();
        //the main symbol will be a complex one.
        CompleteInformation output = new();
        SemanticModel compilationSemanticModel = node.GetSemanticModel(_compilation);
        INamedTypeSymbol symbol = (INamedTypeSymbol)compilationSemanticModel.GetDeclaredSymbol(node)!;
        ResultsModel r = new();
        r.Symbol = symbol;
        output.Result = r;
        r.HasChildren = false; //until proven true.
        PopulateNames(symbol, r, output);
        r.PropertyNames = _properties;
        TypeModel fins = new();
        fins.SpecialCategory = EnumSpecialCategory.Main;
        fins.TypeCategory = EnumTypeCategory.Complex; //this has to be complex.
        fins.ListCategory = EnumListCategory.None;
        fins.SymbolUsed = symbol;
        fins.FileName = symbol.Name;
        _types.Add(fins);
        r.Types = _types;
        return output;
    }
    private void PopulateNames(INamedTypeSymbol symbol, ResultsModel results, CompleteInformation complete) //because we have the symbol captured now.
    {
        if (_lookedAt.Contains(symbol.Name) == true)
        {
            return;
        }
        _lookedAt.Add(symbol.Name);
        var properties = symbol.GetAllPublicProperties();
        properties.RemoveAllOnly(xx =>
        {
            return xx.IsReadOnly ||
            xx.CanBeReferencedByName == false ||
            xx.SetMethod is null;
        });
        foreach (var pp in properties)
        {
            //there can be some we don't add anyways.  maybe don't even need the ignore list because may not even be included anywhere (?)
            if (pp.Name == "Assembly")
            {
                complete.PropertiesToIgnore.Add(pp); //you have to ignore this one somehow or another.
                TypeModel firstIgnore = new();
                firstIgnore.SpecialCategory = EnumSpecialCategory.Ignore;
                firstIgnore.SymbolUsed = pp.Type;
                results.Types.Add(firstIgnore);
                continue;
            }
            _properties.Add(pp.Name);
            //may require rethinking further if there are other properties that cannot be included.
            ITypeSymbol others;
            ITypeSymbol mm;
            if (pp.IsCollection())
            {
                //this is the lists.
                others = pp.GetSingleGenericTypeUsed()!;
                if (others is not null && others.IsCollection())
                {
                    TypeModel fins = new();
                    fins.ListCategory = EnumListCategory.Double;
                    fins.CollectionNameSpace = $"{others.ContainingSymbol.ToDisplayString()}.{others.Name}";
                    fins.CollectionStringName = others.Name;
                    mm = others.GetSingleGenericTypeUsed()!;
                    fins.FileName = $"{others.Name}{others.Name}{mm!.Name}";
                    fins.SubName = $"{others.Name}{mm.Name}";
                    fins.SymbolUsed = mm;
                    fins.TypeCategory = fins.SymbolUsed.GetSimpleCategory();
                    if (_types.Any(x => x.FileName == fins.FileName) == false)
                    {
                        _types.Add(fins);
                    }
                    AddListNames(mm, others, results, complete);
                    continue;
                }
                AddListNames(others!, pp.Type, results, complete);
                continue;
            }
            if (pp.Type.Implements("IPlayerCollection") || pp.Type.Implements("ISimpleList"))
            {
                //this is playercollection or dicelist.
                throw new Exception("Did not implement IPlayerCollection or ISimpleList yet");
            }
            if (pp.Type.Implements("IBoardCollection"))
            {
                AddBoardCollection(pp.Type, results, complete);
                continue;
                //var ii = pp.Type.AllInterfaces.First();
                //var aa = ii.GetSingleGenericTypeUsed();
                //this is iboardcollection.
                
                //none means it has not been handled yet.
                //fins.SpecialCategory = EnumSpecialCategory.Ignore;
                
                //throw new Exception("Did not implement IBoardCollection yet");
            }
            AddSimpleName(pp, results, complete);
        }
    }
    private void AddBoardCollection(ITypeSymbol symbol, ResultsModel results, CompleteInformation complete)
    {
        TypeModel fins = new();
        fins.FileName = symbol.Name;
        fins.SymbolUsed = symbol;
        fins.LoopCategory = EnumLoopCategory.Custom; //hopefully this simple.
        var otherSymbol = (INamedTypeSymbol) symbol.AllInterfaces.First().GetSingleGenericTypeUsed()!;
        fins.SubName = otherSymbol!.Name; //hopefully this simple (?)
        fins.SubSymbol = otherSymbol;
        //may not even care about anything else now.
        if (_types.Any(x => x.FileName == fins.FileName) == false)
        {
            _types.Add(fins);
        }
        
        //can add simple name for this one now.
        AddSimpleName(otherSymbol!, results, complete);
    }
    private void AddListNames(ITypeSymbol symbol, ISymbol collection, ResultsModel results, CompleteInformation complete)
    {
        TypeModel fins = new();
        fins.ListCategory = EnumListCategory.Single;
        string name = collection.Name;
        fins.CollectionNameSpace = $"{collection.ContainingSymbol.ToDisplayString()}.{name}";
        fins.SymbolUsed = symbol;
        fins.SubName = symbol.Name;
        fins.TypeCategory = fins.SymbolUsed.GetSimpleCategory();
        fins.LoopCategory = EnumLoopCategory.Standard;
        fins.FileName = $"{name}{symbol.Name}";
        if (_types.Any(x => x.FileName == fins.FileName) == false)
        {
            _types.Add(fins);
        }
        AddSimpleName(symbol, results, complete);
    }
    private void AddSimpleName(IPropertySymbol symbol, ResultsModel results, CompleteInformation complete)
    {
        AddSimpleName(symbol.Type, results, complete);
    }
    private void AddSimpleName(ITypeSymbol symbol, ResultsModel results, CompleteInformation complete)
    {
        if (symbol.Name == "BasicPileInfo")
        {
            throw new Exception("Did not implement BasicPileInfo yet");
        }
        if (symbol.Implements("IDeckObject"))
        {
            throw new Exception("Did not implement IDeckObject yet");
        }
        TypeModel fins = new();
        fins.ListCategory = EnumListCategory.None;
        fins.SymbolUsed = symbol;
        fins.FileName = symbol.Name;
        fins.TypeCategory = fins.SymbolUsed.GetSimpleCategory();
        fins.LoopCategory = EnumLoopCategory.None;
        var others = symbol.GetSingleGenericTypeUsed();
        results.HasChildren = true;
        if (others is not null)
        {
            throw new Exception("Did not implement when a simple type has generics for now");
        }
        //have not figured out generics yet.
        if (fins.TypeCategory == EnumTypeCategory.Complex)
        {
            if (_types.Any(x => x.FileName == fins.FileName) == false)
            {
                _types.Add(fins);
            }
            fins.SubName = symbol.Name; //well see.
            PopulateNames((INamedTypeSymbol)symbol, results, complete);
            return;
        }
        fins.SubName = symbol.Name; //i think.  can change eventually.
        if (_types.Any(x => x.FileName == fins.FileName) == false)
        {
            _types.Add(fins);
        }
        
    }
}