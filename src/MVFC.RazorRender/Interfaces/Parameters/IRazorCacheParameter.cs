namespace MVFC.RazorRender.Interfaces.Parameters;

/// <summary>
/// Define um parâmetro para renderização Razor que inclui uma chave de cache.
/// </summary>
public interface IRazorCacheParameter : IRazorParameter
{
    /// <summary>
    /// Chave única utilizada para identificar o conteúdo no cache.
    /// </summary>
    string CacheKey { get; }
}