using System;
using System.Collections.Generic;
using System.Text;

namespace GamePackageSaveInfoGenerator;

internal static class DeserializeExtensions
{
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
            w.WriteLine("JsonElement array;")
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
    public static void DeserializeComplex(this ICodeBlock w, TypeModel model, bool property)
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