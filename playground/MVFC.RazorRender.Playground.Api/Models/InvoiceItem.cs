namespace MVFC.RazorRender.Playground.Api.Models;

public sealed record InvoiceItem(
    string Description,
    int Quantity,
    decimal UnitPrice);
