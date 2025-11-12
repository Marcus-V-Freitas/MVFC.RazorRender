namespace MVFC.RazorRender.Interfaces.Services;

/// <summary>
/// Serviço para renderização de HTML Razor com suporte a cache.
/// </summary>
public interface ICacheRazorHtmlRenderService : IBaseHtmlRenderService<IRazorCacheParameter>;