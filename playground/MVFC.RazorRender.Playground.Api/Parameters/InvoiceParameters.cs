namespace MVFC.RazorRender.Playground.Api.Parameters;

public sealed class InvoiceParameters : IRazorCacheParameter
{
    public InvoiceModel Model { get; set; } = default!;

    public string CacheKey => $"invoice-{Model.InvoiceNumber}";
}
