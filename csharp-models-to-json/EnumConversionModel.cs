using System.Collections.Generic;

namespace CSharpModelsToJson;

class EnumConversionModel
{
    public string Identifier { get; set; }
    public Dictionary<string, object> Values { get; set; }
}