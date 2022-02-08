﻿namespace GamePackageSaveInfoGenerator;

internal static class CodeBlockExtensions
{
    public static ICodeBlock PopulateStartArray(this ICodeBlock w, bool property)
    {
        if (property)
        {
            w.WriteLine("writer.WriteStartArray(property);");
        }
        else
        {
            w.WriteLine("writer.WriteStartArray();");
        }
        return w;
    }
    public static ICodeBlock PopulateEndArray(this ICodeBlock w)
    {
        w.WriteLine("writer.WriteEndArray();");
        return w;
    }
    public static ICodeBlock PopulateStartObject(this ICodeBlock w, bool property)
    {
        if (property)
        {
            w.WriteLine("writer.WriteStartObject(property);");
        }
        else
        {
            w.WriteLine("writer.WriteStartObject();");
        }
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
    
    public static ICodeBlock PopulateDeserializeObjectStart(this ICodeBlock w, TypeModel model)
    {
        w.PopulateStartOutput(model)
            .WriteLine("JsonElement temps;")
            .WriteLine("temps = element.GetProperty(property);")
            .PopulateReturnNull(true);
        return w;
    }
    //we want to do a line (but not ready to figure out what is needed)
    //public static ICodeBlock PopulateDeserializeObjectLine(this ICodeBlock w, )
    public static ICodeBlock PopulateReturnNull(this ICodeBlock w, bool property)
    {
        if (property == false)
        {
            w.WriteLine("if (element.ValueKind == global::System.Text.Json.JsonValueKind.Null)");
        }
        else
        {
            w.WriteLine("if (temps.ValueKind == global::System.Text.Json.JsonValueKind.Null)");
        }
        w.WriteCodeBlock(w =>
        {
            w.WriteLine("return null;");
        });
        return w;
    }
    public static ICodeBlock PopulateWriteNull(this ICodeBlock w, bool property)
    {
        w.WriteLine("if (value is null)")
        .WriteCodeBlock(w =>
        {
            if (property == false)
            {
                w.WriteLine("writer.WriteNullValue();");
            }
            else
            {
                w.WriteLine("writer.WriteNull(property);");
            }
            //w.WriteLine("writer.WriteNullValue();")
            w.WriteLine("return;");
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
                .Write(" value)");
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
            .Write(model.FileName)
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