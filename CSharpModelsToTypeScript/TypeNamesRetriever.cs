using System.Collections.Generic;
using System.Linq;

namespace CSharpModelsToTypeScript;
public static class TypeNamesRetriever
{
    public static List<string> GetImportedTypeNames(List<Member> members)
    {
        var complexTypes = members
            .Select(m => ExtractTypeName(m.Type))
            .Distinct()
            .Where(IsComplexType);
        return complexTypes.ToList();
    }

    private static string ExtractTypeName(string type) => type.Replace("?", "").Replace("[]", "");

    private static bool IsComplexType(string typeName) => typeName switch
    {
        "string" => false,
        "int" => false,
        "long" => false,
        "decimal" => false,
        "double" => false,
        "single" => false,
        "float" => false,
        "DateTime" => false,
        "DateTimeOffset" => false,
        "DateOnly" => false,
        "TimeOnly" => false,
        "Guid" => false,
        "bool" => false,
        _ => true
    };
}
