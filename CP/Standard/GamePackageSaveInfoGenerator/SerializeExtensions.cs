using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
namespace GamePackageSaveInfoGenerator;
internal static class SerializeExtensions
{
    public static void SerializeComplex(this ICodeBlock w, TypeModel model, bool property)
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
            return; //for now.
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
}