using System.Collections.Generic;

namespace CSharpModelsToJson;

public class Model
{
    public string ModelName { get; set; }
    public IEnumerable<Member> Members { get; set; }
    public IEnumerable<string> BaseClasses { get; set; }
}