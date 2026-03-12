namespace MVFC.RazorRender.Interfaces.Services;

/// <summary>
/// Base service for generating HTML from Razor components.
/// </summary>
/// <typeparam name="TParameters">
/// Type of parameters used in rendering, which must implement <see cref="IRazorParameter"/>.
/// </typeparam>
public interface IBaseHtmlRenderService<TParameters>
    where TParameters : IRazorParameter
{
    /// <summary>
    /// Generates the HTML of a Razor component asynchronously.
    /// </summary>
    /// <typeparam name="TComponent">Type of the Razor component to be rendered.</typeparam>
    /// <param name="parameters">Parameters for component rendering.</param>
    /// <returns>A task representing the asynchronous operation, containing the generated HTML.</returns>
    public Task<string> GenerateHtmlAsync<TComponent>(TParameters parameters)
        where TComponent : IComponent;
}
