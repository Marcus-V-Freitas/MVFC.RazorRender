namespace MVFC.RazorRender.Interfaces.Services;

/// <summary>
/// Serviço base para geração de HTML a partir de componentes Razor.
/// </summary>
/// <typeparam name="TParameters">
/// Tipo dos parâmetros utilizados na renderização, que deve implementar <see cref="IRazorParameter"/>.
/// </typeparam>
public interface IBaseHtmlRenderService<TParameters>
    where TParameters : IRazorParameter
{
    /// <summary>
    /// Gera o HTML de um componente Razor de forma assíncrona.
    /// </summary>
    /// <typeparam name="TComponent">Tipo do componente Razor a ser renderizado.</typeparam>
    /// <param name="parameters">Parâmetros para a renderização do componente.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo o HTML gerado.</returns>
    Task<string> GenerateHtmlAsync<TComponent>(TParameters parameters)
        where TComponent : IComponent;
}