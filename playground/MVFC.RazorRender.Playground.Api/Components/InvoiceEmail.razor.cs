namespace MVFC.RazorRender.Playground.Api.Components;

public partial class InvoiceEmail : ComponentBase
{
    [Parameter]
    public required InvoiceModel Model { get; set; }
}
