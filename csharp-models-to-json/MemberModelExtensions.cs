
namespace CSharpModelsToJson;
public static class MemberModelExtensions
{
    public static string GetTypeScriptType(this Member typeMemberModel)
    {
        var baseType = ExtractBaseTypeName(typeMemberModel.Type);
        var isBaseTypeNullable = IsNullable(typeMemberModel.Type);
        var isArray = IsArray(typeMemberModel.Type);

        baseType = isBaseTypeNullable ? baseType[..^1] : baseType;
        var typeScriptType = TypeScriptTypeName(baseType);

        var typeScriptNullableType = GenerateTypeScriptNullableTypeString(typeScriptType, isBaseTypeNullable, isArray);
        var isWholeTypeNullable = IsNullable(typeMemberModel.Type);
        return GenerateTypeScriptNullableTypeString(typeScriptNullableType, isWholeTypeNullable, false);
    }

    private static bool IsNullable(string type) => type.EndsWith('?');
    private static bool IsArray(string type) => type.Contains("[]");
    private static string GenerateTypeScriptNullableTypeString(string typeScriptType, bool isNullable, bool isArray)
    {
        var nullableType = isNullable ? $"{typeScriptType} | null" : typeScriptType;
        var requiresParentheses = isNullable && isArray;
        var type = requiresParentheses ? $"({nullableType})" : nullableType;
        return isArray ? $"{type}[]" : type;
    }

    private static string TypeScriptTypeName(string type) => type switch
    {
        "string" => "string",
        "int" => "number",
        "decimal" => "number",
        "double" => "number",
        "single" => "number",
        "float" => "number",
        "DateTime" => "string",
        "DateTimeOffset" => "string",
        "Guid" => "string",
        "bool" => "boolean",
        _ => type
    };

    private static string ExtractBaseTypeName(string type) => type.Replace("[]", "").Replace("??", "?");

    public static string GetLowerCamelCaseName(this Member typeMemberModel) => 
        char.ToLowerInvariant(typeMemberModel.Identifier[0]) + typeMemberModel.Identifier[1..];
}
