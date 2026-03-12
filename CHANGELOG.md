# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0]

### Changed

- Translated all XML summary doc comments from Portuguese to English across all source files
- Rewrote `README.md` in English with expanded documentation (requirements, how it works, interfaces table, cache notes)
- Added `README.pt-br.md` with full Portuguese translation and cross-link to the English version

---

## [1.1.0] - 2025-11-16

### Added

- .NET 10 target framework support (`net10.0`) alongside `net9.0`
- Full project structure: services, interfaces, extension methods, DI registration
- Razor component rendering without cache (`RazorHtmlRenderService` / `IRazorHtmlRenderService`)
- Razor component rendering with hybrid cache (`CacheHtmlRenderService` / `ICacheRazorHtmlRenderService`)
- `AddRazorRender()` and `AddRazorRenderCache()` extension methods for DI setup
- `IRazorParameter` and `IRazorCacheParameter` strongly-typed parameter interfaces
- Test project with Razor view, mocks and integration tests

---

## [1.0.1] - 2025-11-12

### Fixed

- Corrected repository URL in package metadata

---

## [1.0.0] - 2025-11-12

### Added

- Initial release of `MVFC.RazorRender`
- Core project structure with Apache-2.0 license

---

[Unreleased]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.1.0...HEAD
[1.1.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.0.1...v1.1.0
[1.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/releases/tag/v1.0.0
