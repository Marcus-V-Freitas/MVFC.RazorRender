# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [4.0.2] - 2026-04-01

### Added
- Explicitly configured `LangVersion`, `AnalysisLevel`, and `AnalysisMode` settings in `Directory.Build.props` for stricter code analysis.

### Changed
- Refactored `DateTime` to `DateTimeOffset` in `.Mock` and `.Models` to comply with static analysis recommendations.
- Switched AppHost startup to use asynchronous `RunAsync()` instead of `Run()`.
- Renamed `Partial` folder and namespaces to `Partials` in the test project to follow naming conventions.

### Fixed
- Renamed `Directory.Build.target` to `Directory.Build.targets` to be correctly evaluated by MSBuild.
- Addressed SonarQube rule S1075 by removing hardcoded absolute URIs in `MockEntities.cs` and using `UriBuilder` instead.

## [4.0.1] - 2026-04-01

### Changed
- Improved code coverage reporting by explicitly excluding `tests` and `playground` projects in `codecov.yml` and `coverage.runsettings`.

## [4.0.0] - 2026-04-01

### Added
- Integrated **MinVer** for automatic Semantic Versioning based on Git tags.
- Configured `MinVerTagPrefix` as `v` to align with existing project tagging conventions.

### Changed
- Simplified GitHub Actions CI workflow by removing manual version extraction and project patching scripts.
- Optimized MSBuild property functions in `Directory.Build.Props` for more robust project categorization and exclusion (e.g., playground projects).

## [3.1.1] - 2026-03-21

### Changed
- CI/CD workflow refinements for automated publishing and coverage reporting
- Minor adjustments to Codecov configuration for status checks precision

## [3.1.0] - 2026/03/15

### Added

- `playground/` project: runnable ASP.NET Core application demonstrating real-world usage of the library
  - Welcome email endpoint (`GET /render/welcome`) — renders a Razor component to HTML, cached per email address
  - Invoice endpoint (`GET /render/invoice`) — renders an invoice component, cached per invoice number
  - Invoice as string endpoint (`GET /render/invoice/string`) — returns the rendered HTML as a JSON payload, suitable for email dispatch or PDF generation
  - AppHost project using [MVFC.Aspire.Helpers.Redis](https://github.com/Marcus-V-Freitas/MVFC.Aspire.Helpers) to provision Redis via Docker through .NET Aspire
- `AddRazorRenderCache` now accepts an optional `redisConnectionString` parameter
  - When provided, registers `IDistributedCache` via `StackExchange.Redis`, enabling `HybridCache` L2 layer automatically
  - When omitted, only L1 in-memory cache is used — no breaking change for existing callers
- Integration tests for the playground using `DistributedApplicationFactory` (Aspire testing pattern):
  - `ProjectAppHost` — bootstraps the AppHost for test runs
  - `AppHostFixture` — `IAsyncLifetime` fixture managing the test lifecycle
  - `AppHostTests` — end-to-end tests covering all render endpoints, cache hit behavior, and content assertions

### Changed

- `README.md` and `README.pt-BR.md` updated with:
  - New **Playground** section explaining motivation and how to run it
  - New cache configuration examples covering in-memory only and Redis L2
  - Explanation of the two-layer cache architecture (L1 memory / L2 Redis)

---

## [3.0.1] - 2026-03-14

### Changed

- Version adjustment to ensure correct release history.

---

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

[4.0.2]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v4.0.1...v4.0.2
[4.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v4.0.0...v4.0.1
[4.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v3.1.1...v4.0.0
[3.1.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v3.1.0...v3.1.1
[3.1.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v3.0.1...v3.1.0
[3.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v3.0.0...v3.0.1
[3.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v2.0.1...v3.0.0
[2.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v2.0.0...v2.0.1
[2.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.1.0...v2.0.0
[1.1.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.0.1...v1.1.0
[1.0.1]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Marcus-V-Freitas/MVFC.RazorRender/releases/tag/v1.0.0
