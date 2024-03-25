using System.Collections.Generic;

namespace CSharpModelsToJson;

class FileToConvert
{
    public string FileName { get; set; }
    public IEnumerable<Model> Models { get; set; }
    public IEnumerable<EnumConversionModel> Enums { get; set; }
}