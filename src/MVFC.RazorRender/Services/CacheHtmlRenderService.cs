namespace MVFC.RazorRender.Services;

/// <summary>
/// Serviço de renderização de HTML Razor com suporte a cache, utilizando <see cref="HybridCache"/>.
/// </summary>
/// <param name="razorService">Serviço responsável pela renderização Razor sem cache.</param>
/// <param name="cache">Instância do cache híbrido utilizada para armazenar o HTML gerado.</param>
public sealed class CacheHtmlRenderService(IRazorHtmlRenderService razorService, HybridCache cache)
    : BaseHtmlRenderService<IRazorCacheParameter>, ICacheRazorHtmlRenderService
{
    private readonly IRazorHtmlRenderService _razorService = razorService;
    private readonly HybridCache _cache = cache;

    /// <summary>
    /// Gera o HTML de um componente Razor utilizando cache. Se o HTML já estiver em cache, retorna o valor armazenado;
    /// caso contrário, renderiza e armazena o resultado.
    /// </summary>
    /// <typeparam name="TComponent">Tipo do componente Razor a ser renderizado.</typeparam>
    /// <param name="parameters">Parâmetros para a renderização, incluindo a chave de cache.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo o HTML gerado.</returns>
    public override async Task<string> GenerateHtmlAsync<TComponent>(IRazorCacheParameter parameters) =>
        await _cache.GetOrCreateAsync(parameters.CacheKey, async _ =>
                await _razorService.GenerateHtmlAsync<TComponent>(parameters));
}