# Orion-RE Windows notices

## Orion Store attribution

Orion-RE Windows is an unofficial community project inspired by the original Orion Store.

- Upstream project: https://github.com/RookieEnough/Orion-Store
- Original developer: RookieZ (RookieEnough)

Thank you to RookieZ and the Orion Store creators and contributors for their work and inspiration. This project is not affiliated with, sponsored by, or endorsed by them.

## Orion-RE license

Copyright (C) 2026 Abhinav Krishnan and contributors.

Unless a file or component identifies a different license, Orion-RE Windows source code is licensed under GPL-3.0-only. The complete license text is in [LICENSE](LICENSE). SPDX identifiers are included in source files. Source corresponding to each published executable is available in this repository at the release's source tag.

## Catalog metadata

The Windows app catalog includes package identifiers and metadata derived from the community-maintained [winget-pkgs repository](https://github.com/microsoft/winget-pkgs), which is licensed under MIT. Package manifest metadata identifies each publisher and package license. That license applies to the corresponding third-party application, not to Orion-RE.

Orion-RE does not bundle the listed applications or their installers. When a user confirms an install or update, Orion-RE invokes Windows Package Manager using the exact package identifier and the `winget` source; winget downloads the package from its configured source and presents its interactive installer.
