# C# models to TypeScript

This is a tool that creates TypeScript files from your C# domain models, records, enums and types. There's other tools that does this but what makes this one different is that it internally uses [Roslyn (the .NET compiler platform)](https://github.com/dotnet/roslyn) to parse the source files, which removes the need to create and maintain our own parser.

NOte: Currently I havent pushed this to nuget, so you will have to fork the repo and build it yourself.

## Install

``dotnet tool install -g csharpmodelstotypescript``

(Or install it locally if you prefer)

## How to use

Create an options JSON file with the below format & drop it in the folder where you want to run the tool.

```
{
  "InputFolder": "C:\\path-to-folder",
  "OutputFolder": "C:\\path-to-folder",
  "LineEnding": "\n",
  "IndentSize": 2,
  "UseSemiColon": false,
  "UseSingleQuote": true
}
```
Only InputFolder & OutputFolder are required, the others are optional. Default values are as above.

Then run the tool with the below command

``csharpmodelstotypescript csharptotypescript.json``

This tool is easy to use and can be integrated into your build pipeline by adding the below XML to your .csproj file.

```
  <Target Name="csharptotypescript" AfterTargets="AfterBuild">
    <Exec Command="dotnet tool restore" />
    <Exec Command="dotnet csharptotypescript $(ProjectDir)csharptotypescript.json" />
  </Target>

```

## License
Adapted from the below github profile
MIT © [Jonathan Svenheden](https://github.com/svenheden)

[npm-image]: https://img.shields.io/npm/v/csharp-models-to-typescript.svg
[npm-url]: https://npmjs.org/package/csharp-models-to-typescript
