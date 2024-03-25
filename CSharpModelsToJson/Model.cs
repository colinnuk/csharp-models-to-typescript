using System.Collections.Generic;

namespace CSharpModelsToTypeScript;

public class Model
{
    public string ModelName { get; set; }
    public IEnumerable<Member> Members { get; set; }
    public IEnumerable<string> BaseClasses { get; set; }
    public List<string> ImportedTypeNames { get; set; }
}