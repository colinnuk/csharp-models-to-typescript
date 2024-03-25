using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpModelsToJson;

public class ModelCollector : CSharpSyntaxWalker
{
    public readonly List<Model> Models = [];

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var model = CreateModel(node);

        Models.Add(model);
    }

    public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
    {
        var model = CreateModel(node);

        Models.Add(model);
    }

    public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        var fields = node.ParameterList.Parameters
            .Where(field => IsAccessible(field.Modifiers))
            .Where(property => !IsIgnored(property.AttributeLists))
            .Select(ConvertParam);

        var properties = node.Members.OfType<PropertyDeclarationSyntax>()
            .Where(property => IsAccessible(property.Modifiers))
            .Where(property => !IsIgnored(property.AttributeLists))
            .Select(ConvertProperty);

        var members = fields.Concat(properties).ToList();
        
        var model = new Model()
        {
            ModelName = $"{node.Identifier}{node.TypeParameterList?.ToString()}",
            Members = members,
            BaseClasses = node.BaseList?.Types.Select(s => s.ToString()),
            ImportedTypeNames = TypeNamesRetriever.GetImportedTypeNames(members),
        };

        Models.Add(model);
    }

    private static Model CreateModel(TypeDeclarationSyntax node)
    {
        var fields = node.Members.OfType<FieldDeclarationSyntax>()
            .Where(field => IsAccessible(field.Modifiers))
            .Where(property => !IsIgnored(property.AttributeLists))
            .Select(ConvertField);

        var properties = node.Members.OfType<PropertyDeclarationSyntax>()
                            .Where(property => IsAccessible(property.Modifiers))
                            .Where(property => !IsIgnored(property.AttributeLists))
                            .Select(ConvertProperty);

        var members = fields.Concat(properties).ToList();

        return new Model()
        {
            ModelName = $"{node.Identifier}{node.TypeParameterList?.ToString()}",
            Members = members,
            BaseClasses = node.BaseList?.Types.Select(s => s.ToString()),
            ImportedTypeNames = TypeNamesRetriever.GetImportedTypeNames(members),
        };
    }

    private static bool IsIgnored(SyntaxList<AttributeListSyntax> propertyAttributeLists) =>
        propertyAttributeLists.Any(attributeList =>
            attributeList.Attributes.Any(attribute =>
                attribute.Name.ToString().Equals("JsonIgnore")));

    private static bool IsAccessible(SyntaxTokenList modifiers) => modifiers.All(modifier =>
        modifier.ToString() != "const" &&
        modifier.ToString() != "static" &&
        modifier.ToString() != "private"
    );

    private static Member ConvertParam(ParameterSyntax param) => new()
    {
        Identifier = param.Identifier.ToString(),
        Type = param.Type.ToString(),
    };

    private static Member ConvertField(FieldDeclarationSyntax field) => new()
    {
        Identifier = field.Declaration.Variables.First().GetText().ToString(),
        Type = field.Declaration.Type.ToString(),
    };

    private static Member ConvertProperty(PropertyDeclarationSyntax property) => new()
    {
        Identifier = property.Identifier.ToString(),
        Type = property.Type.ToString(),
    };
}