namespace MVFC.RazorRender.Services;

/// <summary>
/// Classe base abstrata para serviços de renderização de HTML a partir de componentes Razor.
/// </summary>
/// <typeparam name="TParameters">
/// Tipo dos parâmetros utilizados na renderização, que deve implementar <see cref="IRazorParameter"/>.
/// </typeparam>
public abstract class BaseHtmlRenderService<TParameters> : IBaseHtmlRenderService<TParameters>
    where TParameters : IRazorParameter
{
    /// <summary>
    /// Gera o HTML de um componente Razor de forma assíncrona.
    /// </summary>
    /// <typeparam name="TComponent">Tipo do componente Razor a ser renderizado.</typeparam>
    /// <param name="parameters">Parâmetros para a renderização do componente.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona, contendo o HTML gerado.</returns>
    public abstract Task<string> GenerateHtmlAsync<TComponent>(TParameters parameters)
        where TComponent : IComponent;

    /// <summary>
    /// Indica se uma propriedade específica deve ser ignorada durante o processamento.
    /// Por padrão, ignora a propriedade <see cref="IRazorCacheParameter.CacheKey"/>.
    /// </summary>
    /// <param name="propertyName">Nome da propriedade a ser verificada.</param>
    /// <returns>
    /// <c>true</c> se a propriedade deve ser ignorada; caso contrário, <c>false</c>.
    /// </returns>
    protected virtual bool SkipSpecificProperties(string propertyName) =>
        propertyName == nameof(IRazorCacheParameter.CacheKey);
}