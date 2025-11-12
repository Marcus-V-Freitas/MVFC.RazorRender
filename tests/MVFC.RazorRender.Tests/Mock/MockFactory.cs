namespace MVFC.RazorRender.Tests.Mock;

public static class MockFactory
{
    public static Post CreatePostMock() =>
        new(
            Id: 1,
            Title: "Título de Exemplo",
            Content: "Este é o conteúdo do post de exemplo.",
            Comments:
            [
                new (Name: "Alice", Age: 25),
                new (Name: "Bob", Age: 32),
                new (Name: "Carol", Age: 28),
            ]);

    public static HybridCacheEntryOptions MockCacheOptions() =>
         new()
         {
             Expiration = TimeSpan.FromMinutes(5)
         };
}