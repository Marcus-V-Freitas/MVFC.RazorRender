namespace MVFC.RazorRender.Interfaces.Services;

/// <summary>
/// Service for rendering Razor HTML with cache support.
/// </summary>
public interface ICacheRazorHtmlRenderService : IBaseHtmlRenderService<IRazorCacheParameter>;