
namespace CSharpModelsToJson;
public static class MemberModelExtensions
{
    public static string GetTypeScriptType(this Member typeMemberModel) => typeMemberModel.Type switch
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
        _ => typeMemberModel.Type
    };

    public static string GetLowerCamelCaseName(this Member typeMemberModel) => 
        char.ToLowerInvariant(typeMemberModel.Identifier[0]) + typeMemberModel.Identifier[1..];
}
