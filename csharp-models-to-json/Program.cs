using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace CSharpModelsToJson;
public class Program
{
    public static void Main(string[] args)
    {
        List<FileToConvert> files = [];

        foreach (string fileName in GetFileNames(args[0]))
        {
            files.Add(ParseFile(fileName));
        }

        string json = JsonConvert.SerializeObject(files);
        System.Console.WriteLine(json);
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