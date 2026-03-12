namespace MVFC.RazorRender.Interfaces.Parameters;

/// <summary>
/// Defines a parameter for Razor rendering that includes a cache key.
/// </summary>
public interface IRazorCacheParameter : IRazorParameter
{
    /// <summary>
    /// Unique key used to identify the content in the cache.
    /// </summary>
    public string CacheKey { get; }
}
