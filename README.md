# Orion-RE Windows

An unofficial, open-source Windows storefront inspired by [Orion Store](https://github.com/RookieEnough/Orion-Store).

[![Download Orion-RE for Windows](https://img.shields.io/badge/Download-Orion--RE.exe-5B5CEB?style=for-the-badge)](https://github.com/abhinav8364-png/Orion-RE-Windows/releases/latest/download/Orion-RE.exe)

**Windows x64:** [Download the latest standalone `Orion-RE.exe`](https://github.com/abhinav8364-png/Orion-RE-Windows/releases/latest/download/Orion-RE.exe) or [browse all releases](https://github.com/abhinav8364-png/Orion-RE-Windows/releases).

## What's included

- Dark Windows desktop storefront with search, categories, featured apps, and package details.
- 606 free/open-source apps with license metadata and exact package IDs; the 500 new listings also include current manifest license links.
- Install and update actions for every catalog entry. Orion asks for confirmation, then opens winget so you can review the package and approve its installer prompts. Orion does not silently run installers.
- Catalog entries use real package IDs; fictional/demo entries without installable packages have been removed.
- Select **View license** in app details where a license source link is available.

The downloadable executable is a self-contained Windows x64 build and does not require a separate .NET runtime. Installing or updating catalog apps requires Windows Package Manager (`winget`), which is included with supported Windows versions.

> **Windows security notice:** Releases are not code-signed. Windows may show a SmartScreen or unknown-publisher warning. Only download releases from this repository, and review the package name and publisher in winget before approving installation.

## Build and test

Requirements: Windows, the .NET 10 SDK, and the Windows Desktop development components.

```powershell
dotnet restore .\Orion-RE-Windows.slnx
dotnet build .\Orion-RE-Windows.slnx --configuration Release
dotnet test .\Orion-RE-Windows.slnx --configuration Release
```

To publish a standalone Windows x64 executable:

```powershell
dotnet publish .\Orion.Windows\Orion.Windows.csproj `
  --configuration Release `
  --runtime win-x64 `
  --self-contained true `
  -p:PublishSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true `
  -p:EnableCompressionInSingleFile=true `
  -p:AssemblyName=Orion-RE `
  --output .\artifacts\publish
```

The executable is written to `artifacts\publish\Orion-RE.exe`.

## Acknowledgements

Thank you to RookieZ (RookieEnough) and the Orion Store creators and contributors for the original project and inspiration. Special thanks to **Abhinav Krishnan** for creating and maintaining this community Windows repository.

Orion-RE Windows is an independent community project; it is not affiliated with or endorsed by the Orion Store creators.

## License

This project is distributed under GPL-3.0-only. The complete license, third-party notices, source attribution, and catalog provenance are in [LICENSE](LICENSE), [NOTICE.md](NOTICE.md), and [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md). Source corresponding to every executable release is available in this repository at the release's source tag.
