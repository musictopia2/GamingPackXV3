namespace CommandsGenerator;
internal class ParserClass
{
    private readonly Compilation _compilation;
    public ParserClass(Compilation compilation)
    {
        _compilation = compilation;
    }
    private BasicList<IPropertySymbol> GetControlProperties(INamedTypeSymbol classSymbol)
    {
        BasicList<IPropertySymbol> output = new();
        //symbol.GetMembers().OfType<IMethodSymbol>().Where(xx => xx.DeclaredAccessibility == Accessibility.Private && xx.MethodKind == MethodKind.Ordinary && xx.HasAttribute(aa.Command.CommandAttribute)).ToBasicList();
        var list = classSymbol.GetMembers().OfType<IPropertySymbol>().Where(xx => xx.DeclaredAccessibility == Accessibility.Public);
        foreach (var item in list)
        {
            if (item.Type.Name == "ControlCommand")
            {
                output.Add(item);
            }
        }
        return output;
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
            info.GenericInfo = symbol.GetGenericString();
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
            //var output = symbol.GetMembers().OfType<IMethodSymbol>().Where(xx => xx.DeclaredAccessibility == Accessibility.Public && xx.MethodKind == MethodKind.Ordinary && xx.HasAttribute(attributeName));
            //return output.ToBasicList();
            //looks like another issue.
            //because if you inherit from controlobservable, then underlying method is not public.
            bool isControl = symbol.InheritsFrom("SimpleControlObservable");
            BasicList<IMethodSymbol> firsts;
            BasicList<IPropertySymbol> controls = new();
            if (isControl == false)
            {
                firsts = symbol.GetPublicMethods(aa.Command.CommandAttribute);
            }
            else
            {
                //it can be protected now since inherited versions can sometimes access the method.
                firsts = symbol.GetMembers().OfType<IMethodSymbol>().Where(xx =>  xx.MethodKind == MethodKind.Ordinary && xx.HasAttribute(aa.Command.CommandAttribute)).ToBasicList();
            }
            if (isControl)
            {
                //needs to figure out the symbol for ControlCommand.
                controls = GetControlProperties(symbol);
                if (controls.Count == 1)
                {
                    info.CommandProperty = controls.Single();
                }
            }
            info.IsControl = isControl;
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
                string? tempName = attributes.AttributePropertyValue<string>(aa.Command.GetNameInfo);
                if (string.IsNullOrWhiteSpace(tempName) == false && command.Category != EnumCommandCategory.Control)
                {
                    command.CannotUseNames = true;
                }
                if (command.Category == EnumCommandCategory.Old)
                {
                    info.NeedsCommandsOnly = true;
                    command.CreateCategory = EnumCreateCategory.Regular;
                }
                else if (command.Category== EnumCommandCategory.Control)
                {
                    info.NeedsCommandsOnly = true;
                    command.CreateCategory = EnumCreateCategory.Container; //still needs container.  however, don't need the method because you are doing differently.
                    if (string.IsNullOrWhiteSpace(tempName) == false)
                    {
                        command.CommandName = tempName!;
                    }
                }
                else
                {
                    info.NeedsCommandContainer = true;
                    command.CreateCategory = EnumCreateCategory.Container;
                }
                command.CanSymbol = m.GetCanSymbol(seconds);
                command.IsProperty = command.CanSymbol is IPropertySymbol;
                if (command.Category == EnumCommandCategory.OutOfTurn) //we need to solve for control now.
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