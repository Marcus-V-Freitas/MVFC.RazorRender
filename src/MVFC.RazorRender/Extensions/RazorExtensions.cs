namespace MVFC.RazorRender.Extensions;

/// <summary>
/// Extension methods to facilitate the configuration and manipulation of HTML rendered via Razor.
/// </summary>
public static class RazorExtensions
{
    /// <summary>
    /// Removes line breaks and decodes HTML entities from generated content.
    /// </summary>
    /// <param name="html">HTML to be cleaned.</param>
    /// <returns>Decoded HTML without line breaks.</returns>
    public static string CleanGeneratedHtml(this string html) =>
         HttpUtility.HtmlDecode(html)
                    .ReplaceLineEndings(string.Empty);

    /// <summary>
    /// Adds the necessary services for Razor rendering to the dependency injection container.
    /// </summary>
    /// <param name="services">Application service collection.</param>
    public static void AddRazorRender(this IServiceCollection services)
    {
        services.AddLogging();
        services.AddTransient<HtmlRenderer>();
        services.AddTransient<IRazorHtmlRenderService, RazorHtmlRenderService>();
    }

    /// <summary>
    /// Adds Razor rendering services with cache support to the dependency injection container.
    /// </summary>
    /// <param name="services">Application service collection.</param>
    /// <param name="action">Action to configure hybrid cache options.</param>
    public static void AddRazorRenderCache(this IServiceCollection services, Action<HybridCacheOptions> action)
    {
        services.AddHybridCache(action);
        services.AddRazorRender();
        services.AddTransient<ICacheRazorHtmlRenderService, CacheHtmlRenderService>();
    }
}