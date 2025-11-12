namespace MVFC.RazorRender.Services;

/// <summary>
/// Serviço responsável pela renderização de componentes Razor em HTML, sem suporte a cache.
/// </summary>
/// <param name="htmlRenderer">Instância do renderizador de HTML utilizado para processar os componentes Razor.</param>
public sealed class RazorHtmlRenderService(HtmlRenderer htmlRenderer)
    : BaseHtmlRenderService<IRazorParameter>, IRazorHtmlRenderService
{
    private readonly HtmlRenderer _htmlRenderer = htmlRenderer;

    /// <summary>
    /// Gera o HTML de um componente Razor de forma assíncrona.
    /// </summary>
    /// <typeparam name="TComponent">Tipo do componente Razor a ser renderizado.</typeparam>
    /// <param name="parameters">Parâmetros para a renderização do componente.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo o HTML gerado.</returns>
    public override async Task<string> GenerateHtmlAsync<TComponent>(IRazorParameter parameters)
    {
        var html = await _htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            var html = await _htmlRenderer.RenderComponentAsync<TComponent>(ConvertToParameterView(parameters));
            return html.ToHtmlString();
        });

        return html.CleanGeneratedHtml();
    }

    /// <summary>
    /// Converte os parâmetros fornecidos em um <see cref="ParameterView"/> para renderização do componente.
    /// </summary>
    /// <typeparam name="TParameters">Tipo dos parâmetros.</typeparam>
    /// <param name="parameters">Instância dos parâmetros a serem convertidos.</param>
    /// <returns>Um <see cref="ParameterView"/> representando os parâmetros do componente.</returns>
    private ParameterView ConvertToParameterView<TParameters>(TParameters parameters) =>
        ParameterView.FromDictionary(ToDictionary(parameters));

    /// <summary>
    /// Converte um objeto de parâmetros em um dicionário de propriedades, ignorando propriedades específicas conforme necessário.
    /// </summary>
    /// <typeparam name="TParameters">Tipo dos parâmetros.</typeparam>
    /// <param name="parametros">Instância dos parâmetros a serem convertidos.</param>
    /// <returns>Dicionário contendo os nomes e valores das propriedades dos parâmetros.</returns>
    private Dictionary<string, object?> ToDictionary<TParameters>(TParameters parametros)
    {
        ArgumentNullException.ThrowIfNull(parametros);

        var dict = new Dictionary<string, object?>();

        foreach (var prop in parametros.GetType().GetProperties())
        {
            if (SkipSpecificProperties(prop.Name))
                continue;

            dict[prop.Name] = prop.GetValue(parametros)!;
        }

        return dict;
    }
}