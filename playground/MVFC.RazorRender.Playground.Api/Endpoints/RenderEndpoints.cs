namespace MVFC.RazorRender.Playground.Api.Endpoints;

public static class RenderEndpoints
{
    public static void MapRenderEndpoints(this WebApplication app)
    {
        app.MapGet("/render/welcome", async (ICacheRazorHtmlRenderService renderer, string email = "marcus@example.com") =>
        {
            var parameters = MockEntities.MockWelcomeEmailParameters(email);
            var html = await renderer.GenerateHtmlAsync<WelcomeEmail>(parameters).ConfigureAwait(false);

            return Results.Content(html, "text/html");
        });

        app.MapGet("/render/invoice", async (ICacheRazorHtmlRenderService renderer, string number = "NF-2026-001") =>
        {
            var parameters = MockEntities.MockInvoiceParameters(number);
            var html = await renderer.GenerateHtmlAsync<InvoiceEmail>(parameters).ConfigureAwait(false);

            return Results.Content(html, "text/html");
        });

        app.MapGet("/render/invoice/string", async (ICacheRazorHtmlRenderService renderer) =>
        {
            var number = "NF-2026-002";
            var parameters = MockEntities.MockInvoiceParameters(number);
            var html = await renderer.GenerateHtmlAsync<InvoiceEmail>(parameters).ConfigureAwait(false);

            return Results.Ok(new
            {
                invoiceNumber = number,
                html
            });
        });
    }
}
