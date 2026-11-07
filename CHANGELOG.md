# Changelog

## [1.0.2] - 2026-11-07

### Added
- Added 500 more Winget-backed FOSS apps with exact package IDs, current license metadata, and upstream license links.
- Added a direct license-source link to catalog app details.
- Included the exact .NET runtime and WPF third-party notices with release assets.

### Changed
- Expanded the licensed open-source catalog from 106 to 606 applications.
- Updated the catalog provenance and release metadata for version 1.0.2.

## [1.0.1] - 2026-11-06

### Added
- Install and update actions are enabled for every catalog entry using verified exact winget package IDs.
- Replaced the remaining fictional demo listings with real FOSS applications and their source-reported licenses.
- Added GPL-3.0-only SPDX identifiers and third-party runtime/dependency notices.

### Changed
- Winget package IDs containing `+` are accepted safely as process arguments.
- Improved GPL source availability, catalog provenance, and third-party license attribution.

## [Unreleased]

### Added
- Initial Windows solution scaffold and project structure
- WPF app foundation with MVVM and dependency-injection wiring
- Settings persistence with safe recovery from corrupted JSON
- Catalog metadata models and validation helpers
- Unit tests for persistence and catalog parsing
- Documentation for licensing, architecture, and roadmap
