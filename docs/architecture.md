# Orion-RE Windows Architecture

This repository starts with a native WinUI 3 foundation and a modular MVVM architecture.

## Goals

- Keep the Orion experience recognizable while adapting to Windows desktop patterns.
- Separate persistence, catalog parsing, app state, and UI concerns.
- Maintain a secure and reviewable installation and download pipeline.
- Preserve licensing and attribution requirements from the upstream Orion project.

## Current foundation

- App host and DI registration
- Main window and MVVM shell
- Settings persistence with corruption fallback
- Catalog metadata models and validation helper
- Test project for persistence and parsing

## Planned phases

1. Data engine and catalog normalization
2. Storefront experience and app details
3. Download queue and installer safety
4. Windows integration and update tracking
5. Release engineering and packaging
