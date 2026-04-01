# MVFC.RazorRender

[![CI](https://github.com/Marcus-V-Freitas/MVFC.RazorRender/actions/workflows/ci.yml/badge.svg)](https://github.com/Marcus-V-Freitas/MVFC.RazorRender/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/Marcus-V-Freitas/MVFC.RazorRender/branch/main/graph/badge.svg)](https://codecov.io/gh/Marcus-V-Freitas/MVFC.RazorRender)
[![License](https://img.shields.io/badge/license-Apache--2.0-blue)](LICENSE)
![Platform](https://img.shields.io/badge/.NET-9_%7C_10-blue?logo=dotnet)
![NuGet Version](https://img.shields.io/nuget/v/MVFC.RazorRender?logo=nuget)
![NuGet Downloads](https://img.shields.io/nuget/dt/MVFC.RazorRender?logo=nuget)

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

## Package

| Package | Downloads |
|---|---|
| [MVFC.RazorRender](src/MVFC.RazorRender/README.md) | ![Downloads](https://img.shields.io/nuget/dt/MVFC.RazorRender?logo=nuget) |

---

## Instalação

Via NuGet Package Manager:

    Install-Package MVFC.RazorRender

Via .NET CLI:

    dotnet add package MVFC.RazorRender

---

## Como Usar

### 1. Configuração dos Serviços

No seu `Program.cs` ou durante a configuração de serviços:

#### Sem Cache

    services.AddRazorRender();

#### Com Cache Híbrido (somente memória)

    services.AddRazorRenderCache(options =>
    {
        options.DefaultEntryOptions = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(5)
        };
    });

> **Observação:** `AddRazorRenderCache` já chama `AddRazorRender` internamente, portanto não é necessário registrar os dois separadamente.

#### Com Cache Híbrido + Redis (L2)

Para persistir o cache entre reinicializações ou compartilhá-lo entre múltiplas instâncias, informe a connection string do Redis:

    services.AddRazorRenderCache(
        action: options =>
        {
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(10)
            };
        },
        redisConnectionString: "localhost:6379"
    );

> **Como as camadas de cache funcionam:**
> - **L1 — Memória:** sempre ativa, ultra-rápida, local ao processo. Nenhuma dependência extra necessária.
> - **L2 — Redis:** opcional. Quando `redisConnectionString` é informado, o `IDistributedCache` é registrado via `StackExchange.Redis` e o `HybridCache` o utiliza automaticamente como segunda camada. Ideal para deployments multi-instância ou quando o cache precisa sobreviver a reinicializações da aplicação.
>
> Se `redisConnectionString` for omitido, apenas L1 (memória) é utilizado.

---

### 2. Modelos e DTOs de Parâmetros

Crie um DTO de parâmetro implementando `IRazorParameter` (sem cache) ou `IRazorCacheParameter` (com cache).

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

> **Importante:** A propriedade `CacheKey` de `IRazorCacheParameter` é automaticamente excluída do `ParameterView` passado ao componente Razor. Portanto, não é necessário declará-la como parâmetro Blazor no componente.

---

### 3. Injeção de Dependência

    public sealed class MeuServico(ICacheRazorHtmlRenderService cacheRazorHtmlRender)
    {
        private readonly ICacheRazorHtmlRenderService _cacheRazorHtmlRender = cacheRazorHtmlRender;
    }

---

### 4. Renderizando um Componente Razor

    var parameters = new CommentParameterDto(Model: model, CacheKey: "post-123");

    string html = await _cacheRazorHtmlRender.GenerateHtmlAsync<BlogView>(parameters);

---

### 5. Exemplo de Teste Automatizado

    [Fact(DisplayName = "Renderização Razor HTML de Post")]
    public async Task Test_Post_RazorHtmlRender()
    {
        var parameters = new CommentParameterDto(Model: model, CacheKey: "test");
        string html = await _cacheRazorHtmlRender.GenerateHtmlAsync<BlogView>(parameters);
        Assert.NotNull(html);
    }

---

## Playground

O diretório `playground/` contém uma aplicação ASP.NET Core executável que demonstra o uso real da biblioteca de ponta a ponta.

**Motivação:** em vez de ler trechos de código isolados, o playground permite executar componentes Razor sendo renderizados para HTML, observar o cache funcionando na prática e usá-lo como ponto de partida para sua própria integração. Cada endpoint HTTP mapeia um cenário concreto: e-mail de boas-vindas, nota fiscal e exportação de HTML como string.

**Dependências para rodar o playground:**

- Workload do [.NET Aspire](https://learn.microsoft.com/aspire) instalado (`dotnet workload install aspire`)
- Docker (para subir o Redis via Aspire)
- [MVFC.Aspire.Helpers.Redis](https://github.com/Marcus-V-Freitas/MVFC.Aspire.Helpers) — usado no AppHost para provisionar o container Redis

**Como executar:**

    cd playground/MVFC.RazorRender.Playground.AppHost
    dotnet run

Em seguida, abra o dashboard do Aspire e acesse o serviço `playground`. Endpoints disponíveis:

| Endpoint | Descrição |
|---|---|
| `GET /render/welcome?email=...` | Renderiza e-mail de boas-vindas (cache por e-mail) |
| `GET /render/invoice?number=...` | Renderiza nota fiscal (cache por número) |
| `GET /render/invoice/string` | Retorna o HTML renderizado como string JSON |

> Sem Redis (ou seja, sem rodar o AppHost), o playground continua funcionando usando apenas o cache L1 em memória.

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

## Contribuindo

Veja [CONTRIBUTING.md](CONTRIBUTING.md).

## Licença

[Apache-2.0](LICENSE)
