using System;
using System.Collections.Generic;
using System.Text;

namespace GamePackageSaveInfoGenerator;

internal class EmitClass
{
    private readonly SourceProductionContext _context;
    private readonly ResultsModel _result;
    private readonly Compilation _compilation;
    //for now, not using ignoreproperties.

    private readonly BasicList<IPropertySymbol> _ignoreProperties;
    public EmitClass(SourceProductionContext context, CompleteInformation info, Compilation compilation)
    {
        _context = context;
        _result = info.Result;
        _ignoreProperties = info.PropertiesToIgnore;
        _compilation = compilation;
    }
    public void Emit()
    {
        ProcessG();
        ProcessTypes();
        ProcessRegistration();
    }
    private void ProcessTypes()
    {
        foreach (var item in _result.Types)
        {
            ProcessType(item);
        }
    }
    private void ProcessType(TypeModel model)
    {
        if (model.SpecialCategory == EnumSpecialCategory.Ignore)
        {
            return; //this should take care of the first issue.
        }
        SourceCodeStringBuilder builder = new();
        builder.WriteContext(_compilation, _result, w =>
        {
            if (model.SpecialCategory == EnumSpecialCategory.Main)
            {
                ProcessMainType(w, model);
                return;
            }
            ProcessOtherType(w, model);
            //has to figure out all the other stuff.
        });
        _context.AddSource($"{_result.ClassName}Context.{model.FileName}.g", builder.ToString());
    }
    private void ProcessOtherType(ICodeBlock w, TypeModel model)
    {
        w.PopulateSerialize(_result, model, false, w =>
        {
            Serialize(w, model, false);
        });
        w.PopulateDeserialize(_result, model, false, w =>
        {
            Deserialize(w, model, false);
        });
        w.PopulateSerialize(_result, model, true, w =>
        {
            Serialize(w, model, true);
        });
        w.PopulateDeserialize(_result, model, true, w =>
        {
            Deserialize(w, model, true);
        });
    }
    private void Serialize(ICodeBlock w, TypeModel model, bool property)
    {
        w.SerializeInt(model, property);
        w.SerializeSimpleList(model, property);
        w.SerializeComplex(model, _ignoreProperties, property);
        w.SerializeVector(model, property);
        w.SerializeString(model, property);
        w.SerializeCustomList(model, property);
        //will do other methods here.
    }
    private void Deserialize(ICodeBlock w, TypeModel model, bool property)
    {
        w.DeserializeInt(model, property);
        w.DeserializeSimpleList(model, property);
        w.DeserializeComplex(model, _ignoreProperties, property);
        w.DeserializeVector(model, property);
        w.DeserializeString(model, property);
        w.DeserializeCustomList(model, property);
    }
    private void ProcessMainType(ICodeBlock w, TypeModel model)
    {
        w.PopulateSerialize(_result, model, false, w =>
        {
            if (_result.HasChildren == false)
            {
                w.PopulateStartObject(false);
                w.PopulateEndObject();
                return;
            }
            w.SerializeComplex(model, _ignoreProperties, false);
        });
        w.PopulateDeserialize(_result, model, false, w =>
        {
            if (_result.HasChildren == false)
            {
                w.PopulateStartOutput(model)
                .PopulateReturnOutput();
                return;
            }
            w.DeserializeComplex(model, _ignoreProperties, false);
        });
    }
    private void ProcessRegistration()
    {
        SourceCodeStringBuilder builder = new();
        builder.StartGlobalProcesses(_compilation, "AutoResumeContexts", "GlobalRegistrations", w =>
        {
            string camel = _result.ContextName.ChangeCasingForVariable(EnumVariableCategory.PrivateFieldParameter);
            w.WriteLine(w =>
            {
                w.Write("private static ")
                .Write(_result.ContextName)
                .Write(" ")
                .Write(camel)
                .Write(" = new();");
            })
            .WriteLine("public static void Register()")
            .WriteCodeBlock(w =>
            {
                w.WriteLine(w =>
                {
                    w.Write("CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.CustomSerializeHelpers<")
                    .Write(_result.GetGlobalName)
                    //w.PopulateInterface(_result)
                    .Write(">.MasterContext = ")
                    .Write(camel)
                    .Write(";");
                });
            });
        });
        _context.AddSource("GlobalRegistrations.g", builder.ToString());
    }
    private void ProcessG()
    {
        SourceCodeStringBuilder builder = new();
        builder.WriteContext(_compilation, _result, w =>
        {
            SerializeProcess(w);
            DeserializeProcess(w);
        });
        _context.AddSource($"{_result.ContextName}.g", builder.ToString());
    }
    private void SerializeProcess(ICodeBlock w)
    {
        w.WriteLine(w =>
        {
            w.Write("string ")
            .PopulateInterface(_result)
            .Write(".Serialize(")
            .Write(_result.GetGlobalName)
            .Write(" obj)");
        })
        .WriteCodeBlock(w =>
        {
            w.WriteLine("using var ms = new MemoryStream();")
            .WriteLine("using var writer = new global::System.Text.Json.Utf8JsonWriter(ms, new global::System.Text.Json.JsonWriterOptions()")
            .WriteLambaBlock(w =>
            {
                w.WriteLine("Indented = true");
            })
            .WriteLine(w =>
             {
                 w.Write(_result.ClassName)
                 .Write("SerializeHandler(writer");
                 if (_result.HasChildren == false)
                 {
                     w.Write(");");
                 }
                 else
                 {
                     w.Write(", obj);");
                 }
             })
            .WriteLine("writer.Flush();")
            .WriteLine("string jsonString = global::System.Text.Encoding.UTF8.GetString(ms.ToArray());")
            .WriteLine("ms.Close();")
            .WriteLine("return jsonString;");
        });
    }
    private void DeserializeProcess(ICodeBlock w)
    {
        w.WriteLine(w =>
        {
            w.Write(_result.GetGlobalName)
            .Write(" ")
            .PopulateInterface(_result)
            .Write(".Deserialize(string json)");
        })
        .WriteCodeBlock(w =>
        {
            w.WriteLine("using var doc = global::System.Text.Json.JsonDocument.Parse(json);")
            .WriteLine("var element = doc.RootElement;")
            .WriteLine(w =>
            {
                w.Write(_result.ClassName)
                .Write(" output = ")
                .Write(_result.ClassName)
                .Write("DeserializeHandler(");
                if (_result.HasChildren == false)
                {
                    w.Write(");");
                }
                else
                {
                    w.Write("element);");
                }
            })
            .WriteLine("return output;");
        });
    }
}