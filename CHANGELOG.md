# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.0] - 2026-03-14

### Added

- Localized `README.md` files (English and Brazilian Portuguese) for the root and the main project.
- New `BlogView.razor` test view for better coverage of blog-like scenarios.

### Changed

- Updated GitHub Actions CI workflow to include more robust testing and coverage reporting.
- Improved `.editorconfig` for better code consistency.
- Refactored `RazorRenderTest.cs` to use the new `BlogView` for integration tests.

### Removed

- Deleted `.github/workflows/publish.yml` as part of the CI/CD consolidation.

---

## [2.0.1] - 2026-03-12

### Added

- Added GitHub Actions CI workflow for automated testing and coverage reporting
- Added Codecov integration for coverage tracking
- Added SonarAnalyzer.CSharp for static code analysis across projects
- Added `CONTRIBUTING.md` and updated `README.md` with NuGet badges and project links

## [2.0.0] - 2026-03-11

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

[Unreleased]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v3.0.0...HEAD
[3.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v2.0.1...v3.0.0
[2.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v2.0.0...v2.0.1
[2.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.1.0...v2.0.0
[1.1.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.0.1...v1.1.0
[1.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/releases/tag/v1.0.0
