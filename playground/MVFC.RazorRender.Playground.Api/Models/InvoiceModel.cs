namespace MVFC.RazorRender.Playground.Api.Models;

public sealed record InvoiceModel(
    string CustomerName,
    string InvoiceNumber,
    DateTime IssuedAt,
    IReadOnlyList<InvoiceItem> Items)
{
    public decimal Total => Items.Sum(i => i.Quantity * i.UnitPrice);
}
