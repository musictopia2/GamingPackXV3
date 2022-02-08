using System;
using System.Collections.Generic;
using System.Text;

namespace GamePackageSaveInfoGenerator;

internal class ResultsModel
{
    public HashSet<string> PropertyNames { get; set; } = new();
    public HashSet<TypeModel> Types = new();
    public string GetGlobalName => $"global::{NamespaceName}.{ClassName}";
    public string GetGlobalMain => $"global::{NamespaceName}";
    public string NamespaceName => Symbol!.ContainingNamespace.ToDisplayString();
    public string ClassName => Symbol!.Name;
    public string ContextName => $"{Symbol!.Name}Context";
    public INamedTypeSymbol? Symbol { get; set; }
    public bool HasChildren { get; set; } //can act differently if no children.
}
