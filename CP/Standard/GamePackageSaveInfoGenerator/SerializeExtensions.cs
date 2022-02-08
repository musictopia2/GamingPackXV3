namespace GamePackageSaveInfoGenerator;
internal static class SerializeExtensions
{
    private static void PrivateStringStyle(this ICodeBlock w, bool property)
    {
        if (property)
        {
            w.WriteLine("writer.WriteString(property, value);");
        }
        else
        {
            w.WriteLine("writer.WriteStringValue(value);");
        }
    }
    public static void SerializeString(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.ListCategory != EnumListCategory.None)
        {
            return;
        }
        if (model.TypeCategory != EnumTypeCategory.String)
        {
            return;
        }
        PrivateStringStyle(w, property);
    }
    public static void SerializeVector(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.ListCategory != EnumListCategory.None)
        {
            return;
        }
        if (model.TypeCategory != EnumTypeCategory.Vector)
        {
            return;
        }
        w.WriteLine(w =>
        {
            w.Write("string text = $")
            .AppendDoubleQuote("{value.Row} {value.Column}")
            .Write(";");
        });
        if (property)
        {
            w.WriteLine("writer.WriteString(property, text);");
        }
        else
        {
            w.WriteLine("writer.WriteStringValue(text);");
        }
    }
    public static void SerializeComplex(this ICodeBlock w, TypeModel model, BasicList<IPropertySymbol> ignores, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.ListCategory != EnumListCategory.None)
        {
            return;
        }
        if (model.TypeCategory != EnumTypeCategory.Complex)
        {
            return;
        }
        if (model.SpecialCategory == EnumSpecialCategory.Main && property)
        {
            return;
        }
        if (model.SpecialCategory != EnumSpecialCategory.Main)
        {
            w.PopulateWriteNull(property);
        }
        w.PopulateStartObject(property);
        var properties = model.SymbolUsed!.GetAllPublicProperties();
        properties.RemoveAllOnly(xx =>
        {
            return xx.IsReadOnly ||
            xx.CanBeReferencedByName == false ||
            xx.Name == "Assembly" ||
            xx.SetMethod is null;
        });
        //VectorSerializeHandler(writer, "Vector", value.Vector);
        foreach (var p in properties)
        {
            if (p.PropertyIgnored(ignores))
            {
                continue;
            }
            //if there are other properties ignored, will do here.
            //not sure if we even need ignored since i can do here anyways.
            string subs = p.GetSubName();
            w.WriteLine(w =>
            {
                w.Write(subs)
                .Write("SerializeHandler(writer, ")
                .AppendDoubleQuote(p.Name)
                .Write(", value.")
                .Write(p.Name)
                .Write(");");
            });
        }
        w.PopulateEndObject();
    }
    public static void SerializeInt(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.TypeCategory != EnumTypeCategory.Int)
        {
            return;
        }
        if (model.ListCategory != EnumListCategory.None)
        {
            return;
        }
        if (property)
        {
            w.WriteLine("writer.WriteNumber(property, value);");
        }
        else
        {
            w.WriteLine("writer.WriteNumberValue(value);");
        }
    }
    public static void SerializeSimpleList(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.ListCategory != EnumListCategory.Single)
        {
            return;
        }
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        //this is for simple lists.
        if (model.LoopCategory != EnumLoopCategory.Standard)
        {
            return;
        }
        w.PopulateStartArray(property)
        .WriteLine("for (int i = 0; i < value.Count; i++)")
        .WriteCodeBlock(w =>
        {
            w.WriteLine(w =>
            {
                w.Write(model.SubName)
                .Write("SerializeHandler(writer, value[i]);");
            });
        })
        .PopulateEndArray();
    }
    public static void SerializeCustomList(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.LoopCategory != EnumLoopCategory.Custom)
        {
            return;
        }
        w.PopulateStartArray(property)
        .WriteLine("foreach (var item in value)")
        .WriteCodeBlock(w =>
        {
            w.WriteLine(w =>
            {
                w.Write(model.SubName)
                .Write("SerializeHandler(writer, item);");
            });
        })
        .PopulateEndArray();
    }
}