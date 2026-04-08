namespace MVFC.RazorRender.Services;

/// <summary>
/// Service responsible for rendering Razor components into HTML, without cache support.
/// </summary>
/// <param name="htmlRenderer">Instance of the HTML renderer used to process Razor components.</param>
public sealed class RazorHtmlRenderService(HtmlRenderer htmlRenderer)
    : BaseHtmlRenderService<IRazorParameter>, IRazorHtmlRenderService
{
    private readonly HtmlRenderer _htmlRenderer = htmlRenderer;

    /// <summary>
    /// Generates the HTML of a Razor component asynchronously.
    /// </summary>
    /// <typeparam name="TComponent">Type of the Razor component to be rendered.</typeparam>
    /// <param name="parameters">Parameters for component rendering.</param>
    /// <returns>A task representing the asynchronous operation, containing the generated HTML.</returns>
    public override async Task<string> GenerateHtmlAsync<TComponent>(IRazorParameter parameters)
    {
        var html = await _htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var html = await _htmlRenderer.RenderComponentAsync<TComponent>(ConvertToParameterView(parameters)).ConfigureAwait(false);
            return html.ToHtmlString();
        }).ConfigureAwait(false);

        return html.CleanGeneratedHtml();
    }

    /// <summary>
    /// Converts the provided parameters into a <see cref="ParameterView"/> for component rendering.
    /// </summary>
    /// <typeparam name="TParameters">Type of parameters.</typeparam>
    /// <param name="parameters">Instance of the parameters to be converted.</param>
    /// <returns>A <see cref="ParameterView"/> representing the component parameters.</returns>
    private ParameterView ConvertToParameterView<TParameters>(TParameters parameters) =>
        ParameterView.FromDictionary(ToDictionary(parameters));

    /// <summary>
    /// Converts a parameters object into a dictionary of properties, ignoring specific properties as needed.
    /// </summary>
    /// <typeparam name="TParameters">Type of parameters.</typeparam>
    /// <param name="parametros">Instance of parameters to be converted.</param>
    /// <returns>Dictionary containing names and values of coordinate properties.</returns>
    private Dictionary<string, object?> ToDictionary<TParameters>(TParameters parametros)
    {
        ArgumentNullException.ThrowIfNull(parametros);

        var dict = new Dictionary<string, object?>(StringComparer.Ordinal);

        foreach (var prop in parametros.GetType().GetProperties())
        {
            if (SkipSpecificProperties(prop.Name))
                continue;

            dict[prop.Name] = prop.GetValue(parametros)!;
        }

        return dict;
    }
}
