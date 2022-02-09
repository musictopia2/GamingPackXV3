﻿namespace GamePackageSaveInfoGenerator;
internal static class DeserializeExtensions
{
    private static void PrivateStringStyle(this ICodeBlock w, bool property)
    {
        if (property)
        {
            w.WriteLine("string output = element.GetProperty(property).GetString()!;");
        }
        else
        {
            w.WriteLine("string output = element.GetString()!;");
        }
        w.PopulateReturnOutput();
    }
    public static void DeserializeString(this ICodeBlock w, TypeModel model, bool property)
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
    public static void DeserializeVector(this ICodeBlock w, TypeModel model, bool property)
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
        if (property)
        {
            w.WriteLine("string firsts = element.GetProperty(property).GetString()!;")
            .WriteLine(w =>
            {
                w.Write("var list = firsts.Split(")
                .AppendDoubleQuote(" ")
                .Write(");");
            });
        }
        else
        {
            w.WriteLine(w =>
            {
                w.Write("var list = element.GetString()!.Split(")
                .AppendDoubleQuote(" ")
                .Write(");");
            });
        }
        w.WriteLine("var row = int.Parse(list[0]);")
        .WriteLine("var column = int.Parse(list[1]);")
        .WriteLine("return new(row, column);");
    }
    public static void DeserializeInt(this ICodeBlock w, TypeModel model, bool property)
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
            w.WriteLine("int output = element.GetProperty(property).GetInt32();");
        }
        else
        {
            w.WriteLine("int output = element.GetInt32();");
        }
        w.PopulateReturnOutput();
    }
    public static void DeserializeCustomList(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.LoopCategory != EnumLoopCategory.Custom)
        {
            return;
        }
        w.WriteLine(w =>
        {
            w.BasicListWrite()
            .CustomGenericWrite(w =>
            {
                w.SymbolFullNameWrite(model.SubSymbol!);
            })
            .Write(" temp = new();");
        });
        if (property)
        {
            w.WriteLine("global::System.Text.Json.JsonElement array;")
            .WriteLine("array = element.GetProperty(property);")
            .WriteLine("foreach (var item in array.EnumerateArray())")
            .WriteCodeBlock(w =>
            {
                w.PopulateCustomList(model);
            });
        }
        else
        {
            w.WriteLine("foreach (var item in element.EnumerateArray())")
            .WriteCodeBlock(w =>
            {
                w.PopulateCustomList(model);
            });
        }
        w.WriteLine(w =>
        {
            w.PopulateFullClassName(model)
            .Write(" output = new(temp);");
        })
        .PopulateReturnOutput();
    }
    private static void PopulateCustomList(this ICodeBlock w, TypeModel model)
    {
        w.WriteLine(w =>
        {
            w.Write("temp.Add(")
            .Write(model.SubName)
            .Write("DeserializeHandler(item)!);");
        });
    }
    public static void DeserializeSimpleList(this ICodeBlock w, TypeModel model, bool property)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return;
        }
        if (model.ListCategory != EnumListCategory.Single)
        {
            return;
        }
        //this is for simple lists.
        if (model.LoopCategory != EnumLoopCategory.Standard)
        {
            return; //for now.
        }
        w.PopulateStartOutput(model);
        if (property)
        {
            w.WriteLine("global::System.Text.Json.JsonElement array;")
            .WriteLine("array = element.GetProperty(property);")
            .WriteLine("foreach (var item in array.EnumerateArray())")
            .WriteCodeBlock(w =>
            {
                w.PopulateListOutput(model);
            });
        }
        else
        {
            w.WriteLine("foreach (var item in element.EnumerateArray())")
            .WriteCodeBlock(w =>
            {
                w.PopulateListOutput(model);
            });
        }
        w.PopulateReturnOutput();
    }
    private static void PopulateListOutput(this ICodeBlock w, TypeModel model)
    {
        w.WriteLine(w =>
        {
            w.Write("output.Add(")
            .Write(model.SubName)
            .Write("DeserializeHandler(item));");
        });
    }
    public static void DeserializeComplex(this ICodeBlock w, TypeModel model, BasicList<IPropertySymbol> ignores, bool property)
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
        if (property)
        {
            w.PopulateDeserializeObjectStart(model);
        }
        else
        {
            w.PopulateStartOutput(model);
            if (model.SpecialCategory != EnumSpecialCategory.Main)
            {
                w.PopulateReturnNull(false);
            }
        }

        //well see if i am on the right track.
        var properties = model.SymbolUsed!.GetAllPublicProperties();
        properties.RemoveAllOnly(xx =>
        {
            return xx.IsReadOnly ||
            xx.CanBeReferencedByName == false ||
            xx.Name == "Assembly" ||
            xx.SetMethod is null;
        });
        string variableName;
        if (property)
        {
            variableName = "temps";
        }
        else
        {
            variableName = "element";
        }
        foreach (var p in properties)
        {
            if (p.PropertyIgnored(ignores))
            {
                continue;
            }
            string subs = p.GetSubName();
            w.PopulateDeserializeLine(subs, variableName, p);
        }
        w.PopulateReturnOutput();
    }
    private static void PopulateDeserializeLine(this ICodeBlock w, string subName, string variableName, IPropertySymbol p)
    {
        w.WriteLine(w =>
        {
            w.Write("output.")
            .Write(p.Name)
            .Write(" = ")
            .Write(subName)
            .Write("DeserializeHandler(")
            .Write(variableName)
            .Write(", ")
            .AppendDoubleQuote(p.Name)
            .Write(");");
        });
    }


}