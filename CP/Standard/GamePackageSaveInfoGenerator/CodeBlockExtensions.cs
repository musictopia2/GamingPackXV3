namespace GamePackageSaveInfoGenerator;

internal static class CodeBlockExtensions
{
    public static ICodeBlock PopulateStartObject(this ICodeBlock w)
    {
        w.WriteLine("writer.WriteStartObject();");
        return w;
    }
    public static ICodeBlock PopulateEndObject(this ICodeBlock w)
    {
        w.WriteLine("writer.WriteEndObject();");
        return w;
    }
    //SaveInfo output = new();
    public static ICodeBlock PopulateStartOutput(this ICodeBlock w, TypeModel model)
    {
        w.WriteLine(w =>
        {
            w.PopulateFullClassName(model)
            .Write(" output = new();");
        });
        return w;
    }
    public static ICodeBlock PopulateReturnOutput(this ICodeBlock w)
    {
        w.WriteLine("return output;");
        return w;
    }
    //if (element.ValueKind == global::System.Text.Json.JsonValueKind.Null)
    //    {
    //        return null;
    //    }
    public static ICodeBlock PopulateReturnNull(this ICodeBlock w)
    {
        w.WriteLine("if (element.ValueKind == global::System.Text.Json.JsonValueKind.Null)")
        .WriteCodeBlock(w =>
        {
            w.WriteLine("return null;");
        });
        return w;
    }
    public static ICodeBlock PopulateWriteNull(this ICodeBlock w)
    {
        w.WriteLine("if (value is null)")
        .WriteCodeBlock(w =>
        {
            w.WriteLine("writer.WriteNullValue();")
            .WriteLine("return;");
        });
        return w;
    }
    public static void PopulateSerialize(this ICodeBlock w, ResultsModel result, TypeModel model, bool hasProperty, Action<ICodeBlock> action)
    {
        if (hasProperty && model.SpecialCategory == EnumSpecialCategory.Main)
        {
            return; //because there are errors.
        }
        w.WriteLine(w =>
        {
            w.Write("private static void ")
            .Write(model.FileName)
            .Write("SerializeHandler(global::System.Text.Json.Utf8JsonWriter writer");
            if (hasProperty == false && result.HasChildren == false)
            {
                w.Write(")");
            }
            if (hasProperty)
            {
                w.Write(", string property");
            }
            if (result.HasChildren == true)
            {
                w.Write(", ")
                .PopulateFullClassName(model)
                .Write(")");
            }
        })
        .WriteCodeBlock(w =>
        {
            action.Invoke(w);
        });
    }
    public static void PopulateDeserialize(this ICodeBlock w, ResultsModel result, TypeModel model, bool hasProperty, Action<ICodeBlock> action)
    {
        if (hasProperty && model.SpecialCategory == EnumSpecialCategory.Main)
        {
            return;
        }
        w.WriteLine(w =>
        {
            w.Write("private static ")
            .PopulateFullClassName(model)
            .Write(" ")
            .Write(model.TypeName)
            .Write("DeserializeHandler(");
            if (result.HasChildren == false)
            {
                w.Write(")");
                return;
            }
            w.Write("global::System.Text.Json.JsonElement element");
            if (hasProperty)
            {
                w.Write(", string property)");
            }
            else
            {
                w.Write(")");
            }
        })
        .WriteCodeBlock(w =>
        {
            action.Invoke(w);
        });
    }
}