using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpModelsToTypeScript;

class EnumCollector : CSharpSyntaxWalker
{
    public readonly List<EnumModel> Enums = [];

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
        var values = new Dictionary<string, object>();

        foreach (var member in node.Members)
        {
            values[member.Identifier.ToString()] = member.EqualsValue?.Value.ToString() ?? member.Identifier.ToString();
        }

        Enums.Add(new EnumModel()
        {
            Identifier = node.Identifier.ToString(),
            Values = values
        });
    }
}