# Third-party notices

Orion-RE Windows is distributed under GPL-3.0-only; see [LICENSE](LICENSE). This file records third-party components included in the application and their license sources.

## NuGet dependencies

The following application dependencies are distributed under the MIT License:

- CommunityToolkit.Mvvm 8.4.0 — https://github.com/CommunityToolkit/dotnet
- Microsoft.Extensions.DependencyInjection 10.0.0 — https://github.com/dotnet/runtime
- Microsoft.Extensions.Hosting 10.0.0 — https://github.com/dotnet/runtime

MIT License:

Copyright (c) .NET Foundation and Contributors

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

## .NET runtime

The standalone Windows release is self-contained and includes the .NET 10 and Windows Desktop runtime components. They are provided under their respective licenses and notices:

- .NET license: https://github.com/dotnet/runtime/blob/main/LICENSE.TXT
- .NET third-party notices: https://github.com/dotnet/runtime/blob/main/THIRD-PARTY-NOTICES.TXT
- Windows Presentation Foundation source and license: https://github.com/dotnet/wpf

## Catalog applications

Orion-RE does not redistribute catalog applications. The catalog records each FOSS package's license and publisher; installs and updates are downloaded on demand from the configured winget source after user confirmation. Each package remains subject to its own upstream license.
