namespace CommandsGenerator;
internal class ParserClass
{
    private readonly Compilation _compilation;
    public ParserClass(Compilation compilation)
    {
        _compilation = compilation;
    }
    public BasicList<CompleteInfo> GetResults(IEnumerable<ClassDeclarationSyntax> list)
    {
        BasicList<CompleteInfo> output = new();
        foreach (var item in list)
        {
            SemanticModel compilationSemanticModel = item.GetSemanticModel(_compilation);
            INamedTypeSymbol symbol = (INamedTypeSymbol)compilationSemanticModel.GetDeclaredSymbol(item)!;
            CompleteInfo info = new();
            info.ClassSymbol = symbol;
            info.HasPartialClass = item.IsPartial();
            string methodUsed = "CreateCommands";
            var method1 = symbol.GetSpecificMethod(methodUsed, 0);
            info.HasPartialCreateCommandsOnly = method1 is not null;
            var method2 = symbol.GetSpecificMethod(methodUsed, 1);
            if (method2 is null)
            {
                info.ContainerName = "";
            }
            else
            {
                var ss = method2.Parameters.Single();
                if (ss.Type.ContainingNamespace.ToDisplayString() == "BasicGameFrameworkLibrary.CommandClasses" && ss.Type.Name == "CommandContainer")
                {
                    info.ContainerName = ss.Name; //i think
                }
                else
                {
                    info.ContainerName = "";
                }
            }
            var firsts = symbol.GetPublicMethods(aa.Command.CommandAttribute);
            var seconds = symbol.GetCompleteCanList();
            foreach (var m in firsts)
            {
                CommandInfo command = new();
                command.MethodSymbol = m;
                if (m.ReturnType.Name == "AsyncTask" || m.ReturnType.Name == "Task")
                {
                    command.IsAsync = true;
                }
                else if (m.ReturnType.Name != "Void")
                {
                    command.WrongReturnType = true;
                }
                m.TryGetAttribute(aa.Command.CommandAttribute, out var attributes);
                command.Category = attributes.AttributePropertyValue<EnumCommandCategory>(aa.Command.GetCategoryInfo);
                if (command.Category == EnumCommandCategory.Old)
                {
                    info.NeedsCommandsOnly = true;
                    command.CreateCategory = EnumCreateCategory.Regular;
                }
                else
                {
                    info.NeedsCommandContainer = true;
                    command.CreateCategory = EnumCreateCategory.Container;
                }
                command.CanSymbol = m.GetCanSymbol(seconds);
                command.IsProperty = command.CanSymbol is IPropertySymbol;
                if (command.Category == EnumCommandCategory.OutOfTurn || command.Category == EnumCommandCategory.Control)
                {
                    command.NotImplemented = true;
                }
                if (m.Parameters.Count() > 1)
                {
                    command.MiscError = EnumMiscCategory.TooMany;
                }
                else if (command.CanSymbol is IPropertySymbol && m.Parameters.Count() == 1)
                {
                    command.MiscError = EnumMiscCategory.MisMatch;
                }
                else if (command.CanSymbol is IMethodSymbol ss)
                {
                    if (ss.Parameters.Count() != m.Parameters.Count())
                    {
                        command.MiscError = EnumMiscCategory.MisMatch;
                    }
                }
                if (m.Parameters.Count() == 1)
                {
                    command.ParameterUsed = m.Parameters.Single().Type; //don't care about the variable of the parameter but we care about the underlying type.
                    if (command.CanSymbol is IMethodSymbol ss && command.MiscError == EnumMiscCategory.None)
                    {
                        if (ss.Parameters.Single().Type.IsTypeEqual(m.Parameters.Single().Type) == false)
                        {
                            command.InvalidCast = true;
                        }
                    }
                }
                command.MethodName = command.MethodSymbol.Name.Replace("Async", "");
                info.Commands.Add(command);
            }
            output.Add(info);
        }
        return output;
    }
}