namespace MVFC.RazorRender.Services;

/// <summary>
/// Abstract base class for HTML rendering services from Razor components.
/// </summary>
/// <typeparam name="TParameters">
/// Type of parameters used in rendering, which must implement <see cref="IRazorParameter"/>.
/// </typeparam>
public abstract class BaseHtmlRenderService<TParameters> : IBaseHtmlRenderService<TParameters>
    where TParameters : IRazorParameter
{
    /// <summary>
    /// Generates the HTML of a Razor component asynchronously.
    /// </summary>
    /// <typeparam name="TComponent">Type of the Razor component to be rendered.</typeparam>
    /// <param name="parameters">Parameters for component rendering.</param>
    /// <returns>A task representing the asynchronous operation, containing the generated HTML.</returns>
    public abstract Task<string> GenerateHtmlAsync<TComponent>(TParameters parameters)
        where TComponent : IComponent;

    /// <summary>
    /// Indicates whether a specific property should be ignored during processing.
    /// By default, ignores the <see cref="IRazorCacheParameter.CacheKey"/> property.
    /// </summary>
    /// <param name="propertyName">Name of the property to be checked.</param>
    /// <returns>
    /// <c>true</c> if the property should be ignored; otherwise, <c>false</c>.
    /// </returns>
    protected virtual bool SkipSpecificProperties(string propertyName) =>
        propertyName == nameof(IRazorCacheParameter.CacheKey);
}