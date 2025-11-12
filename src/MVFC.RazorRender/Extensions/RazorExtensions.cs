namespace MVFC.RazorRender.Extensions;

/// <summary>
/// Métodos de extensão para facilitar a configuração e manipulação de HTML renderizado via Razor.
/// </summary>
public static class RazorExtensions
{
    /// <summary>
    /// Remove quebras de linha e decodifica entidades HTML do conteúdo gerado.
    /// </summary>
    /// <param name="html">HTML a ser limpo.</param>
    /// <returns>HTML decodificado e sem quebras de linha.</returns>
    public static string CleanGeneratedHtml(this string html) =>
         HttpUtility.HtmlDecode(html)
                    .ReplaceLineEndings(string.Empty);

    /// <summary>
    /// Adiciona os serviços necessários para renderização Razor ao contêiner de injeção de dependência.
    /// </summary>
    /// <param name="services">Coleção de serviços da aplicação.</param>
    public static void AddRazorRender(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddTransient<HtmlRenderer>();
        services.AddTransient<IRazorHtmlRenderService, RazorHtmlRenderService>();
    }

    /// <summary>
    /// Adiciona serviços de renderização Razor com suporte a cache ao contêiner de injeção de dependência.
    /// </summary>
    /// <param name="services">Coleção de serviços da aplicação.</param>
    /// <param name="action">Ação de configuração das opções de cache híbrido.</param>
    public static void AddRazorRenderCache(this IServiceCollection services, Action<HybridCacheOptions> action)
    {
        services.AddHybridCache(action);
        services.AddRazorRender();
        services.AddTransient<ICacheRazorHtmlRenderService, CacheHtmlRenderService>();
    }
}