namespace CSharpModelsToTypeScript;

public class Options
{
    public required string InputFolder { get; init; }
    public required string OutputFolder { get; init; }
    public string LineEnding { get; init; } = "\n";
    public int IndentSize { get; init; } = 2;
    public bool UseSemiColon { get; init; } = false;
    public bool UseSingleQuote { get; init; } = true;
}
