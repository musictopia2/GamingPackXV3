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
        //if there is a list involved, has to show the list part as well.
        if (model.ListCategory == EnumListCategory.None)
        {
            w.Write(model.GetGlobalNameSpace)
                .Write(".")
                .Write(model.TypeName);
            return w;
        }
        if (model.ListCategory == EnumListCategory.Single)
        {
            //this is single list
            w.GlobalWrite()
            .Write(model.CollectionNameSpace)
            .Write("<")
            .Write(model.GetGlobalNameSpace)
            .Write(".")
            .Write(model.TypeName)
            .Write(">");
            return w;
        }
        if (model.ListCategory == EnumListCategory.Double)
        {
            w.GlobalWrite()
                .Write(model.CollectionNameSpace)
                .Write("<")
                .GlobalWrite()
                .Write(model.CollectionNameSpace)
                .Write("<")
                .Write(model.GetGlobalNameSpace)
                .Write(".")
                .Write(model.TypeName)
                .Write(">>");
            return w;
        }
        throw new Exception("List Not Implemented");
    }
}
