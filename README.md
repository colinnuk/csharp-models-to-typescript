# C# models to TypeScript

This is a tool that creates TypeScript files from your C# domain models, records, enums and types. There's other tools that does this but what makes this one different is that it internally uses [Roslyn (the .NET compiler platform)](https://github.com/dotnet/roslyn) to parse the source files, which removes the need to create and maintain our own parser.


## Install



## How to use

Create an options JSON file with the below format

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
Only InputFolder & OutputFolder are required.




## License
Adapted from the below github profile
MIT © [Jonathan Svenheden](https://github.com/svenheden)

[npm-image]: https://img.shields.io/npm/v/csharp-models-to-typescript.svg
[npm-url]: https://npmjs.org/package/csharp-models-to-typescript
