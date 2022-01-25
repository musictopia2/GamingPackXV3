namespace GamePackageDIGenerator;
internal class EmitClass
{
    private readonly SourceProductionContext _context;
    private readonly Compilation _compilation;
    private readonly BasicList<FirstInformation> _list;
    public EmitClass(SourceProductionContext context, Compilation compilation, BasicList<FirstInformation> list)
    {
        _context = context;
        _compilation = compilation;
        _list = list;
    }
    private void ProcessFinishDIRegistrations(ICodeBlock w)
    {
        w.WriteLine("Func<object> action;")
                .WriteLine("List<Type> types;");
        foreach (var item in _list)
        {
            w.WriteLine("action = () =>")
            .WriteCodeBlock(w =>
            {
                if (item.Constructors.Count == 0)
                {
                    w.WriteLine(w =>
                    {
                        w.Write("return new ")
                       .SymbolFullNameWrite(item.MainClass!)
                       .Write("();");
                    });
                }
                else
                {
                    int index = 0;
                    BasicList<string> variables = new();
                    w.WriteLine("object output;");
                    foreach (var c in item.Constructors)
                    {
                        w.WriteLine(w =>
                        {
                            w.Write("output = container.LaterGetObject(typeof(")
                            .SymbolFullNameWrite(c)
                            .Write("));");
                        });
                        string value = $"item{index}";
                        variables.Add(value);
                        w.WriteLine(w =>
                        {
                            w.SymbolFullNameWrite(c)
                            .Write(" ")
                            .Write(value)
                            .Write(" = (")
                            .SymbolFullNameWrite(c)
                            .Write(")output;");
                        });
                        index++;
                    }
                    w.WriteLine(w =>
                    {
                        w.Write("return new ")
                        .SymbolFullNameWrite(item.MainClass!)
                        .Write("(");
                        w.InitializeFromCustomList(variables, (w, fins) =>
                        {
                            w.Write(fins);
                        });
                        w.Write(");");
                    });
                }
            }, endSemi: true)
            .WriteLine("types = new()")
            .WriteCodeBlock(w =>
            {
                BasicList<INamedTypeSymbol> assignments = item.Assignments.ToBasicList();
                assignments.Add(item.MainClass!); //you can always assign that as well.
                        w.InitializeFromCustomList(assignments, (w, assignment) =>
                {
                    w.PopulateTypeOf(assignment);
                });
            }, endSemi: true);
            w.WriteLine(w =>
            {
                w.Write("container.LaterRegister(typeof(")
                .SymbolFullNameWrite(item.MainClass!)
                .Write("), types, action);");
            });
        }
    }
    public void EmitBasic()
    {
        //finishDIRegistrations has to change because of parameters now.
        SourceCodeStringBuilder builder = new();
        builder.StartGlobalProcesses(_compilation, "DIFinishProcesses", "GlobalDIFinishClass", w =>
        {
            w.WriteLine("public static void FinishDIRegistrations(global::BasicGameFrameworkLibrary.DIContainers.IGamePackageGeneratorDI container)")
            .WriteCodeBlock(w =>
            {
                ProcessFinishDIRegistrations(w);
            });
        });
        _context.AddSource($"BasicFinishDI.g", builder.ToString());
    }
    public void EmitAttributes()
    {
        if (_list.Count == 0)
        {
            return; //there was none.  means can ignore.
        }
        SourceCodeStringBuilder builder = new();
        builder.StartGlobalProcesses(_compilation, "DIFinishProcesses", "GlobalDIFinishClass", w =>
        {
            w.WriteLine("public static void AutoRegisterClasses(global::BasicGameFrameworkLibrary.DIContainers.IGamePackageDIContainer container)")
            .WriteCodeBlock(w =>
            {
                RegisterBasics(w);
                ProcessFinishDIRegistrations(w);
            });
        });
        _context.AddSource($"AttributesFinishDI.g", builder.ToString());
    }
    private void RegisterBasics(ICodeBlock w)
    {
        foreach (var item in _list)
        {
            if (item.Category == EnumCategory.Error)
            {
                _context.RaiseDuplicateException(item.MainClass!.Name);
                continue;
            }
            if (item.Category == EnumCategory.None)
            {
                continue;
            }
            w.WriteLine(w =>
            {
                w.Write("container.");
                if (item.Category == EnumCategory.Singleton)
                {
                    w.Write("RegisterSingleton");
                }
                else if (item.Category == EnumCategory.Instance)
                {
                    w.Write("RegisterInstanceType");
                }
                w.Write(");");
            });
        }
    }
}