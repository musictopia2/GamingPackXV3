namespace GamePackageSaveInfoGenerator;
internal static class SourceContextExtensions
{
    public static void RaiseMoreThanOneSaveInfo(this SourceProductionContext context)
    {
        string information = "Only one class is supported per assembly for SaveInfo";
        context.ReportDiagnostic(Diagnostic.Create(RaiseException(information, "MultipleSaveInfo"), Location.None));
    }
    private static DiagnosticDescriptor RaiseException(string information, string id) => new(id,
        "Could not create helpers",
        information,
        "CustomID",
        DiagnosticSeverity.Error,
        true);
}