﻿using System.Collections.Generic;

namespace CSharpModelsToTypeScript;

class FileToConvert
{
    public string FileName { get; set; }
    public List<Model> Models { get; set; }
    public List<EnumModel> Enums { get; set; }
}