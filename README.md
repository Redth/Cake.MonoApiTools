## Cake.MonoApiTools

A set of aliases for http://cakebuild.net to help with running Mono's API Tools (mono-api-info, mono-api-diff, mono-api-html) for creating XML files with API Info from .NET assemblies, and creating XML, HTML and Markdown diffs of API Info files.

You can easily reference Cake.MonoApiTools directly in your build script via a cake addin:

```csharp
#addin nuget:?package=Cake.MonoApiTools
```

### Aliases

```csharp
// for a single assembly
MonoApiInfo(FilePath dotNetAssembly, FilePath outputFile);
MonoApiInfo(FilePath dotNetAssembly, FilePath outputFile, MonoApiInfoToolSettings settings);

// for multiple assemblies
MonoApiInfo(FilePath[] assemblies, FilePath outputFile);
MonoApiInfo(FilePath[] assemblies, FilePath outputFile, MonoApiInfoToolSettings settings);
```

```csharp
// for exporting an XML diff
MonoApiDiff(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile);
MonoApiDiff(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile, MonoApiDiffToolSettings settings);
```

```csharp
// for exporting an HTML diff
MonoApiHtml(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile);
MonoApiHtml(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile, MonoApiHtmlToolSettings settings);
// for exporting a colorized HTML diff
MonoApiHtmlColorized(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile);

// for exporting an Markdown diff
MonoApiMarkdown(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile);
// for exporting a colorized Markdown diff
MonoApiMarkdownColorized(FilePath previousApiInfoFile, FilePath newApiInfoFile, FilePath outputFile);
```

### The MIT License (MIT)

    Copyright (c) 2018 Jonathan Dick
    Copyright (c) 2018 Matthew Leibowitz

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.