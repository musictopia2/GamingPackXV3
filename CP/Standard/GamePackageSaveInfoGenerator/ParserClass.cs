using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamePackageSaveInfoGenerator;

internal class ParserClass
{
    private readonly Compilation _compilation;
    public ParserClass(Compilation compilation)
{
        _compilation = compilation;
    }
    public CompleteInformation GetResults(ClassDeclarationSyntax node)
    {
        //the main symbol will be a complex one.
        CompleteInformation output = new();
        SemanticModel compilationSemanticModel = node.GetSemanticModel(_compilation);
        INamedTypeSymbol symbol = (INamedTypeSymbol)compilationSemanticModel.GetDeclaredSymbol(node)!;
        ResultsModel r = new();
        r.HasChildren = false; //for now, its false until i figure out the children stuff.
        r.Symbol = symbol;
        output.Result = r;
        //we still have to analyze the properties.
        
        TypeModel fins = new();
        fins.SpecialCategory = EnumSpecialCategory.Main;
        fins.TypeCategory = EnumTypeCategory.Complex; //this has to be complex.
        fins.ListCategory = EnumListCategory.None;
        fins.SymbolUsed = symbol;
        fins.FileName = symbol.Name;
        r.Types.Add(fins); //this will not do any fluent stuff.
        return output;
    }
}