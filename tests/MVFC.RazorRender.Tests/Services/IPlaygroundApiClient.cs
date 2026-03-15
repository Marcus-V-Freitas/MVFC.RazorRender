namespace MVFC.RazorRender.Tests.Services;

internal interface IPlaygroundApiClient
{
    [Get("/render/welcome")]
    internal Task<ApiResponse<string>> GetWelcomeAsync([Query] string email = "marcus@example.com");

    [Get("/render/invoice")]
    internal Task<ApiResponse<string>> GetInvoiceAsync([Query] string number = "NF-2026-001");

    [Get("/render/invoice/string")]
    internal Task<ApiResponse<string>> GetInvoiceAsStringAsync();
}
