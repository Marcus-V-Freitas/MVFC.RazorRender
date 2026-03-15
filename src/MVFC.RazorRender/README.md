# MVFC.RazorRender

[![CI](https://github.com/Marcus-V-Freitas/MVFC.RazorRender/actions/workflows/ci.yml/badge.svg)](https://github.com/Marcus-V-Freitas/MVFC.RazorRender/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/Marcus-V-Freitas/MVFC.RazorRender/branch/main/graph/badge.svg)](https://codecov.io/gh/Marcus-V-Freitas/MVFC.RazorRender)
[![License](https://img.shields.io/badge/license-Apache--2.0-blue)](LICENSE)
![Platform](https://img.shields.io/badge/.NET-9%20%7C%2010-blue)
![NuGet Version](https://img.shields.io/nuget/v/MVFC.RazorRender)
![NuGet Downloads](https://img.shields.io/nuget/dt/MVFC.RazorRender)

A library for rendering Razor/Blazor components into pure HTML, with support for hybrid cache and easy integration via .NET Dependency Injection (DI). Ideal for scenarios such as email generation, PDF reports, or exporting dynamic content server-side.

> [Leia em Português (pt-BR)](./README.pt-br.md)

---

## Features

- Render Razor/Blazor components to pure HTML strings
- Hybrid cache support (`HybridCache`) to optimize repeated renders
- Simple DI integration with a single method call
- Strongly-typed parameter model via `IRazorParameter` / `IRazorCacheParameter`
- Works for email generation, reports, dynamic content export, and more

---

## Requirements

- .NET 9.0+
- The project containing your `.razor` files must use the **Razor SDK**:

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        <IsPackable>false</IsPackable>
    </PropertyGroup>
    ...
</Project>
```

---

## Package

| Package | Downloads |
|---|---|
| [MVFC.RazorRender](src/MVFC.RazorRender/README.md) | ![Downloads](https://img.shields.io/nuget/dt/MVFC.RazorRender) |

***

## Installation

Via NuGet Package Manager:

```sh
Install-Package MVFC.RazorRender
```

Via .NET CLI:

```sh
dotnet add package MVFC.RazorRender
```

---

## How to Use

### 1. Service Registration

In your `Program.cs` or service configuration:

#### Without Cache

```csharp
services.AddRazorRender();
```

#### With Hybrid Cache

```csharp
services.AddRazorRenderCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(5)
    };
});
```

> **Note:** `AddRazorRenderCache` internally calls `AddRazorRender`, so you do not need to register both independently.

---

### 2. Defining Models and Parameter DTOs

Create a parameter DTO by implementing `IRazorParameter` (no cache) or `IRazorCacheParameter` (with cache).

```csharp
// Parameter DTO for cache-aware rendering
public sealed record CommentParameterDto(Post Model, string CacheKey) : IRazorCacheParameter;

// Domain models
public sealed record Post(int Id, string Title, string Content, IReadOnlyList<Comment> Comments);
public sealed record Comment(string Name, int Age);

// Razor view component
public partial class BlogView : ComponentBase
{
    [Parameter]
    public required Post Model { get; set; }
}
```

> **Important:** `CacheKey` in `IRazorCacheParameter` is automatically excluded from the `ParameterView` passed to the Razor component, so you don't need to declare it as a Blazor parameter.

---

### 3. Dependency Injection

Inject the desired service into your class:

```csharp
public sealed class MyService(
    IRazorHtmlRenderService razorRenderService,        // Without cache
    ICacheRazorHtmlRenderService cacheRazorHtmlRender) // With cache
{
    private readonly IRazorHtmlRenderService _razorRenderService = razorRenderService;
    private readonly ICacheRazorHtmlRenderService _cacheRazorHtmlRender = cacheRazorHtmlRender;

    // ...
}
```

---

### 4. Rendering a Razor Component

```csharp
var model = MockFactory.CreatePostMock();
var parameters = new CommentParameterDto(Model: model, CacheKey: "post-123");

// Without cache
string html = await _razorRenderService.GenerateHtmlAsync<BlogView>(parameters);

// With cache (returns cached result on subsequent calls with the same key)
string htmlCache = await _cacheRazorHtmlRender.GenerateHtmlAsync<BlogView>(parameters);
```

---

### 5. Automated Test Example

```csharp
[Fact(DisplayName = "Razor HTML rendering of Post")]
public async Task Test_Post_RazorHtmlRender()
{
    var model = MockFactory.CreatePostMock();
    var parameters = new CommentParameterDto(Model: model, CacheKey: "test");

    string html = await _razorRenderService.GenerateHtmlAsync<BlogView>(parameters);

    Assert.NotNull(html);
}
```

---

## Key Interfaces & Classes

| Type | Description |
|------|-------------|
| `IRazorHtmlRenderService` | Renders Razor components to HTML without cache |
| `ICacheRazorHtmlRenderService` | Renders Razor components to HTML with hybrid cache |
| `IRazorParameter` | Marker interface for render parameters |
| `IRazorCacheParameter` | Extends `IRazorParameter` with a `CacheKey` property |
| `BaseHtmlRenderService<T>` | Abstract base class shared by both service implementations |
| `RazorExtensions` | Extension methods: `AddRazorRender()` and `AddRazorRenderCache()` |

---

## How it Works

1. Your parameter DTO's public properties are automatically reflected and mapped into a `ParameterView`.
2. If the parameter implements `IRazorCacheParameter`, the `CacheKey` property is excluded from the component parameters (it's only used as the cache lookup key).
3. The rendered HTML is decoded and line endings are stripped via `CleanGeneratedHtml()` for a clean, single-line output.

---

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

[Apache-2.0](LICENSE)
