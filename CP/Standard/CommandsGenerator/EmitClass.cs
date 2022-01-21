namespace CommandsGenerator;
internal class EmitClass
{
    private readonly SourceProductionContext _context;
    private readonly BasicList<CompleteInfo> _list;
    public EmitClass(SourceProductionContext context, BasicList<CompleteInfo> list)
    {
        _context = context;
        _list = list;
    }
    private void ProcessErrors()
    {
        foreach (var item in _list)
        {
            if (item.HasPartialClass == false)
            {
                _context.RaiseNoPartialClassException(item.ClassSymbol!.Name);
            }
            if (item.HasPartialCreateCommandsOnly == false && item.NeedsCommandsOnly)
            {
                _context.RaiseNoCreateCommandsRegularException(item.ClassSymbol!.Name);
            }
            if (item.ContainerName == "" && item.NeedsCommandContainer)
            {
                _context.RaiseNoCreateCommandsContainerException(item.ClassSymbol!.Name);
            }
            foreach (var c in item.Commands)
            {
                if (c.MiscError == EnumMiscCategory.MisMatch)
                {
                    _context.RaiseMismatchParameters(item.ClassSymbol!.Name, c.MethodSymbol!.Name);
                    c.ReportedError = true;
                }
                if (c.MiscError == EnumMiscCategory.TooMany)
                {
                    _context.RaiseTooManyParameters(item.ClassSymbol!.Name, c.MethodSymbol!.Name);
                    c.ReportedError = true;
                }
                if (c.WrongReturnType)
                {
                    _context.RaiseWrongReturnType(item.ClassSymbol!.Name, c.MethodSymbol!.Name);
                    c.ReportedError = true;
                }
                if (c.NotImplemented)
                {
                    _context.RaiseNotImplemented(item.ClassSymbol!.Name, c.MethodSymbol!.Name, c.Category.GetEnumText());
                    c.ReportedError = true;
                }
                if (c.InvalidCast)
                {
                    _context.RaiseInvalidCast(item.ClassSymbol!.Name, c.MethodSymbol!.Name);
                    c.ReportedError = true;
                }
            }
        }
    }
    public void Emit()
    {
        ProcessErrors();
        foreach (var item in _list)
        {
            if (item.HasPartialClass == false)
            {
                continue;
            }
            SourceCodeStringBuilder builder = new();
            builder.WriteLine("#nullable enable")
                .WriteLine(w =>
                {
                    w.Write("namespace ")
                    .Write(item.ClassSymbol!.ContainingNamespace)
                    .Write(";");
                })
            .WriteLine(w =>
            {
                w.Write("public partial class ")
                .Write(item.ClassSymbol!.Name);
            })
            .WriteCodeBlock(w =>
            {
                WriteCompleteCommands(w, item);
                if (item.NeedsCommandsOnly && item.HasPartialCreateCommandsOnly)
                {
                    w.WriteLine("partial void CreateCommands()")
                    .WriteCodeBlock(w =>
                    {
                        WriteCommandsAlone(w, item);
                    });
                }
                if (item.NeedsCommandContainer && item.ContainerName != "")
                {
                    w.WriteLine(w =>
                    {
                        w.Write("partial void CreateCommands(global::BasicGameCommandsV1Library.CommandClasses.CommandContainer ");
                        w.Write(item.ContainerName)
                        .Write(")");
                    })
                    .WriteCodeBlock(w =>
                    {
                        WriteCommandsWithContainer(w, item);
                    });
                }
            });
            _context.AddSource($"{item.ClassSymbol!.Name}.Command.g", builder.ToString());
        }
    }
    private void WriteCompleteCommands(ICodeBlock w, CompleteInfo info)
    {
        foreach (var item in info.Commands)
        {
            if (item.ReportedError == false)
            {
                w.WriteLine(w =>
                {
                    w.Write("public ")
                    .AppendCommandType(item.Category)
                    .AppendCommandName(item)
                    .Write(";");
                });
            }
        }
    }
    private void WriteCommandsAlone(ICodeBlock w, CompleteInfo info)
    {
        foreach (var item in info.Commands)
        {
            WriteCommandsAlone(w, item);
        }
    }
    private void WriteCommandsAlone(ICodeBlock w, CommandInfo command)
    {
        if (command.ReportedError || command.CreateCategory == EnumCreateCategory.Container)
        {
            return;
        }
        w.WriteLine(w =>
        {
            w.AppendCommandName(command)
            .StartNewCommandMethod()
            .PopulateRestCommand(command)
            .EndNewCommandMethod();
        });
    }
    private void WriteCommandsWithContainer(ICodeBlock w, CompleteInfo info)
    {
        foreach (var item in info.Commands)
        {
            WriteCommandsWithContainer(w, info, item);
        }
    }
    private void WriteCommandsWithContainer(ICodeBlock w, CompleteInfo info, CommandInfo command)
    {
        if (command.ReportedError || command.CreateCategory == EnumCreateCategory.Regular)
        {
            return;
        }
        //will rethink once i figure out how to support other command types
        w.WriteLine(w =>
        {
            w.AppendCommandName(command)
            .StartNewCommandMethod()
            .Write(info.ContainerName)
            .Write(", ")
            .PopulateRestCommand(command)
            .EndNewCommandMethod();
        });
    }
}