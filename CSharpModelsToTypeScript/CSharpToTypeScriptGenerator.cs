using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpModelsToTypeScript;
public class CSharpToTypeScriptGenerator(Options options)
{
    public Options Options { get; } = options;

    public void GenerateTypeScriptForModel(Model model)
    {
        var sb = new StringWriter()
        {
            NewLine = Options.LineEnding
        };
        
        if (model.ImportedTypeNames.Count != 0)
        {
            foreach (var importName in model.ImportedTypeNames)
            {
                sb.WriteLine($"import {{ {importName} }} from './{importName}'{GetStatementEnd()}");
            }
            sb.WriteLine();
        }

        sb.WriteLine($"export interface {model.ModelName} {{");
        foreach (var member in model.Members)
        {
            sb.WriteLine($"{GetIndentString()}{member.GetLowerCamelCaseName()}: {member.GetTypeScriptType()}{GetStatementEnd()}");
        }
        sb.WriteLine("}");

        var filePath = Path.Combine(Options.OutputFolder, $"{model.ModelName}.ts");
        Console.WriteLine($"CSharpModelsToTypeScript: Writing to {filePath}");
        var text = sb.ToString();
        File.WriteAllText(filePath, text);
    }

    public void GenerateTypeScriptForEnum(EnumModel enumModel)
    {
        var sb = new StringWriter()
        {
            NewLine = Options.LineEnding
        };
        sb.WriteLine($"export enum {enumModel.Identifier} {{");
        foreach (var value in enumModel.Values)
        {
            sb.WriteLine($"{GetIndentString()}{value.Key} = {GetQuote()}{value.Value}{GetQuote()}{GetLastCharacter(value.Key, enumModel.Values)}");
        }
        sb.WriteLine("}");

        var filePath = Path.Combine(Options.OutputFolder, $"{enumModel.Identifier}.ts");
        Console.WriteLine($"CSharpModelsToTypeScript: Writing to {filePath}");
        var text = sb.ToString();
        File.WriteAllText(filePath, text);
    }

    private static string GetLastCharacter(string key, Dictionary<string, object> values) => key == values.Keys.Last() ? string.Empty : ",";
    private string GetIndentString() => new(' ', Options.IndentSize);
    private string GetStatementEnd() => Options.UseSemiColon ? ";" : string.Empty;
    private string GetQuote() => Options.UseSingleQuote ? "'" : "\"";
}
