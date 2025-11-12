# MVFC.RazorRender

Biblioteca para renderização de componentes Razor em HTML, com suporte a cache híbrido e integração facilitada via Dependency Injection (DI). Permite transformar componentes Blazor em HTML para cenários como geração de e-mails, relatórios ou exportação de conteúdo dinâmico.

**Observação:**  
*  Para que a renderização de componentes Razor funcione corretamente, é necessário que o seu projeto com os arquivos razor inclua o arquivo `.Razor` no seu `.csproj`.  
Exemplo:

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

## Funcionalidades

- Renderização de componentes Razor/Blazor em HTML puro
- Suporte a cache híbrido para otimizar performance
- Integração simples com DI do .NET
- Ideal para geração de e-mails, relatórios, exportação de conteúdo, etc.

## Instalação

Via NuGet:

```sh
Install-Package MVFC.RazorRender
```

Ou via .NET CLI:

```sh
dotnet add package MVFC.RazorRender
```

## Como Usar

### 1. Configuração dos Serviços

No seu `Program.cs` ou durante a configuração de serviços:

#### Sem Cache

```csharp
services.AddRazorRender();
```

#### Com Cache

```csharp
services.AddRazorRenderCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(5)
    };
});
```

### 2. Exemplo de DTO, Modelos e View

```csharp
// DTO de parâmetro para renderização com cache
public sealed record CommentParameterDto(Post Model, string CacheKey) : IRazorCacheParameter;

// Modelos
public sealed record Post(int Id, string Title, string Content, IReadOnlyList<Comment> Comments);
public sealed record Comment(string Name, int Age);

// View Razor
public partial class BlogView : ComponentBase
{
 [Parameter]
 public required Post Model { get; set; }
}
```

### 3. Injeção de dependência

```csharp
public sealed class SeuServico(
    IRazorHtmlRenderService razorRenderService,        // Serviço sem cache
    ICacheRazorHtmlRenderService cacheRazorHtmlRender) // Serviço com cache
{
    private readonly IRazorHtmlRenderService _razorRenderService = razorRenderService;
    private readonly ICacheRazorHtmlRenderService _cacheRazorHtmlRender = cacheRazorHtmlRender;
...

...
}

```

### 4. Renderizando um Componente Razor
```csharp
var model = MockFactory.CreatePostMock();
var parameters = new CommentParameterDto(Model: model, CacheKey: "test");

// Renderização sem cache
string html = await _razorRenderService.GenerateHtmlAsync<BlogView>(parameters);

// Renderização com cache
string htmlCache = await _cacheRazorHtmlRender.GenerateHtmlAsync<BlogView>(parameters);
```

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

## Principais Interfaces

- `IRazorHtmlRenderService`: Renderização Razor sem cache
- `ICacheRazorHtmlRenderService`: Renderização Razor com cache híbrido
- `IRazorParameter` e `IRazorCacheParameter`: Parâmetros para renderização

## Licença

Este projeto está licenciado sob a licença Apache-2.0.

---