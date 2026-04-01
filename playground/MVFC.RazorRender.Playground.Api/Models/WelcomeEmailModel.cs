namespace MVFC.RazorRender.Playground.Api.Models;

public sealed record WelcomeEmailModel(
    string Name,
    string Email,
    DateTimeOffset CreatedAt,
    string ActivationUrl);
