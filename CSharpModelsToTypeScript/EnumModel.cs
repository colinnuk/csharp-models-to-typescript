using System.Collections.Generic;

namespace CSharpModelsToTypeScript;

public class EnumModel
{
    public string Identifier { get; set; }
    public Dictionary<string, object> Values { get; set; }
}