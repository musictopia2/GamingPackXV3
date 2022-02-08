using System;
using System.Collections.Generic;
using System.Text;

namespace GamePackageSaveInfoGenerator;

internal static class WriterExtensions
{
    public static IWriter PopulateInterface(this IWriter w, ResultsModel model)
    {
        w.Write("global::CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.ICustomJsonContext<")
            .Write(model.GetGlobalName)
            .Write(">");
        return w;
    }
    public static IWriter PopulateFullClassName(this IWriter w, TypeModel model)
    {
        w.Write(model.GetGlobalNameSpace)
            .Write(".")
            .Write(model.TypeName);
        return w;
    }
}