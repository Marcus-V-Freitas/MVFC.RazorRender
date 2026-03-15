# MVFC.RazorRender

[![CI](https://github.com/Marcus-V-Freitas/MVFC.RazorRender/actions/workflows/ci.yml/badge.svg)](https://github.com/Marcus-V-Freitas/MVFC.RazorRender/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/Marcus-V-Freitas/MVFC.RazorRender/branch/main/graph/badge.svg)](https://codecov.io/gh/Marcus-V-Freitas/MVFC.RazorRender)
[![License](https://img.shields.io/badge/license-Apache--2.0-blue)](LICENSE)
![Platform](https://img.shields.io/badge/.NET-9%20%7C%2010-blue)
![NuGet Version](https://img.shields.io/nuget/v/MVFC.RazorRender)
![NuGet Downloads](https://img.shields.io/nuget/dt/MVFC.RazorRender)

Uma biblioteca para renderização de componentes Razor/Blazor em HTML puro, com suporte a cache híbrido e integração facilitada com o sistema de Injeção de Dependência (DI) do .NET. Ideal para cenários como geração de e-mails, relatórios em PDF ou exportação de conteúdo dinâmico no lado do servidor.

> [Read in English](./README.md)

---

## Funcionalidades

- Renderização de componentes Razor/Blazor em strings HTML puras
- Suporte a cache híbrido (`HybridCache`) para otimizar renderizações repetidas
- Integração simples com DI em uma única chamada
- Modelo de parâmetros fortemente tipado via `IRazorParameter` / `IRazorCacheParameter`
- Ideal para geração de e-mails, relatórios, exportação de conteúdo dinâmico e mais

---

## Requisitos

- .NET 9.0+
- O projeto contendo seus arquivos `.razor` deve obrigatoriamente usar o **Razor SDK**:

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

## Instalação

Via NuGet Package Manager:

```sh
Install-Package MVFC.RazorRender
```

Via .NET CLI:

```sh
dotnet add package MVFC.RazorRender
```

---

## Como Usar

### 1. Configuração dos Serviços

No seu `Program.cs` ou durante a configuração de serviços:

#### Sem Cache

```csharp
services.AddRazorRender();
```

#### Com Cache Híbrido

```csharp
services.AddRazorRenderCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(5)
    };
});
```

> **Observação:** `AddRazorRenderCache` já chama `AddRazorRender` internamente, portanto não é necessário registrar os dois separadamente.

---

### 2. Modelos e DTOs de Parâmetros

Crie um DTO de parâmetro implementando `IRazorParameter` (sem cache) ou `IRazorCacheParameter` (com cache).

```csharp
// DTO de parâmetro para renderização com cache
public sealed record CommentParameterDto(Post Model, string CacheKey) : IRazorCacheParameter;

// Modelos de domínio
public sealed record Post(int Id, string Title, string Content, IReadOnlyList<Comment> Comments);
public sealed record Comment(string Name, int Age);

// View Razor
public partial class BlogView : ComponentBase
{
    [Parameter]
    public required Post Model { get; set; }
}
```

> **Importante:** A propriedade `CacheKey` de `IRazorCacheParameter` é automaticamente excluída do `ParameterView` passado ao componente Razor. Portanto, não é necessário declará-la como parâmetro Blazor no componente.

---

### 3. Injeção de Dependência

Injete o serviço desejado na sua classe:

```csharp
public sealed class MeuServico(
    IRazorHtmlRenderService razorRenderService,        // Serviço sem cache
    ICacheRazorHtmlRenderService cacheRazorHtmlRender) // Serviço com cache
{
    private readonly IRazorHtmlRenderService _razorRenderService = razorRenderService;
    private readonly ICacheRazorHtmlRenderService _cacheRazorHtmlRender = cacheRazorHtmlRender;

    // ...
}
```

---

### 4. Renderizando um Componente Razor

```csharp
var model = MockFactory.CreatePostMock();
var parameters = new CommentParameterDto(Model: model, CacheKey: "post-123");

// Sem cache
string html = await _razorRenderService.GenerateHtmlAsync<BlogView>(parameters);

// Com cache (retorna o resultado do cache em chamadas subsequentes com a mesma chave)
string htmlCache = await _cacheRazorHtmlRender.GenerateHtmlAsync<BlogView>(parameters);
```

---

### 5. Exemplo de Teste Automatizado

```csharp
[Fact(DisplayName = "Renderização Razor HTML de Post")]
public async Task Test_Post_RazorHtmlRender()
{
    var model = MockFactory.CreatePostMock();
    var parameters = new CommentParameterDto(Model: model, CacheKey: "test");

    string html = await _razorRenderService.GenerateHtmlAsync<BlogView>(parameters);

    Assert.NotNull(html);
}
```

---

## Principais Interfaces e Classes

| Tipo | Descrição |
|------|-----------|
| `IRazorHtmlRenderService` | Renderização de componentes Razor em HTML sem cache |
| `ICacheRazorHtmlRenderService` | Renderização com suporte a cache híbrido |
| `IRazorParameter` | Interface marcadora para parâmetros de renderização |
| `IRazorCacheParameter` | Estende `IRazorParameter` adicionando a propriedade `CacheKey` |
| `BaseHtmlRenderService<T>` | Classe base abstrata compartilhada pelas implementações dos serviços |
| `RazorExtensions` | Métodos de extensão: `AddRazorRender()` e `AddRazorRenderCache()` |

---

## Como Funciona

1. As propriedades públicas do seu DTO de parâmetro são automaticamente refletidas e mapeadas para um `ParameterView`.
2. Se o parâmetro implementar `IRazorCacheParameter`, a propriedade `CacheKey` é excluída dos parâmetros passados ao componente (ela é usada apenas como chave de busca no cache).
3. O HTML gerado é decodificado e as quebras de linha são removidas via `CleanGeneratedHtml()`, resultando em uma saída limpa em linha única.

---

## Contribuindo

Veja [CONTRIBUTING.md](CONTRIBUTING.md).

## Licença

[Apache-2.0](LICENSE)
