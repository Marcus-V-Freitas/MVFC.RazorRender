namespace MVFC.RazorRender.Services;

/// <summary>
/// Razor HTML rendering service with cache support, using <see cref="HybridCache"/>.
/// </summary>
/// <param name="razorService">Service responsible for Razor rendering without cache.</param>
/// <param name="cache">Hybrid cache instance used to store generated HTML.</param>
public sealed class CacheHtmlRenderService(IRazorHtmlRenderService razorService, HybridCache cache)
    : BaseHtmlRenderService<IRazorCacheParameter>, ICacheRazorHtmlRenderService
{
    private readonly IRazorHtmlRenderService _razorService = razorService;
    private readonly HybridCache _cache = cache;

    /// <summary>
    /// Generates the HTML of a Razor component using cache. If the HTML is already in cache, returns the stored value;
    /// otherwise, renders and stores the result.
    /// </summary>
    /// <typeparam name="TComponent">Type of the Razor component to be rendered.</typeparam>
    /// <param name="parameters">Parameters for rendering, including the cache key.</param>
    /// <returns>A task representing the asynchronous operation, containing the generated HTML.</returns>
    public override async Task<string> GenerateHtmlAsync<TComponent>(IRazorCacheParameter parameters) =>
        await _cache.GetOrCreateAsync(parameters.CacheKey, async _ =>
                await _razorService.GenerateHtmlAsync<TComponent>(parameters).ConfigureAwait(false)).ConfigureAwait(false);
}
