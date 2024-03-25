using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpModelsToJson;

class EnumCollector : CSharpSyntaxWalker
{
    public readonly List<EnumConversionModel> Enums = new List<EnumConversionModel>();

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
        var values = new Dictionary<string, object>();

        foreach (var member in node.Members)
        {
            values[member.Identifier.ToString()] = member.EqualsValue?.Value.ToString() ?? member.Identifier.ToString();
        }

        Enums.Add(new EnumConversionModel()
        {
            Identifier = node.Identifier.ToString(),
            Values = values
        });
    }
}