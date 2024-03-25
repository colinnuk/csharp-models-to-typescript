using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;

namespace CSharpModelsToTypeScript;
public class Program
{
    public static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile(args[0], false, false)
            .Build();
        Options options = config.Get<Options>();

        List<FileToConvert> files = [];

        foreach (string fileName in GetFileNames(options.InputFolder))
        {
            files.Add(ParseFile(fileName));
        }

        var generator = new CSharpToTypeScriptGenerator(options);
        foreach (var file in files)
        {
            foreach (var model in file.Models)
            {
                generator.GenerateTypeScriptForModel(model);
            }

            foreach (var enumModel in file.Enums)
            {
                generator.GenerateTypeScriptForEnum(enumModel);
            }
        }
    }

    private static List<string> GetFileNames(string directoryName)
    {
        var filePaths = Directory.GetFiles(directoryName, "*.cs", SearchOption.AllDirectories).Where(p => !p.Contains("\\obj\\"));
        return [.. filePaths];
    }

    private static FileToConvert ParseFile(string path)
    {
        string source = File.ReadAllText(path);
        SyntaxTree tree = CSharpSyntaxTree.ParseText(source);
        var root = (CompilationUnitSyntax)tree.GetRoot();

        var modelCollector = new ModelCollector();
        var enumCollector = new EnumCollector();

        modelCollector.Visit(root);
        enumCollector.Visit(root);

        return new FileToConvert()
        {
            FileName = Path.GetFullPath(path),
            Models = modelCollector.Models,
            Enums = enumCollector.Enums
        };
    }
}