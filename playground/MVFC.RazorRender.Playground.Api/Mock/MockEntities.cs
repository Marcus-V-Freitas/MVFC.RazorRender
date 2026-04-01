namespace MVFC.RazorRender.Playground.Api.Mock;

public static class MockEntities
{
    public static InvoiceParameters MockInvoiceParameters(string number) =>
        new()
        {
            Model = new InvoiceModel(
                CustomerName: "Acme Corp",
                InvoiceNumber: number,
                IssuedAt: DateTimeOffset.Now,
                Items:
                [
                    new("Licença anual MVFC.Suite", 1, 1200.00m),
                    new("Suporte premium", 12, 150.00m),
                    new("Onboarding", 1, 500.00m)
                ])
        };

    public static WelcomeEmailParameters MockWelcomeEmailParameters(string email) =>
         new()
         {
             Model = new WelcomeEmailModel(
                 Name: "Marcus",
                 Email: email,
                 CreatedAt: DateTimeOffset.Now,
                 ActivationUrl: new UriBuilder(Uri.UriSchemeHttps, "example.com", -1, "activate/abc123").Uri.AbsoluteUri)
         };
}
