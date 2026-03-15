namespace MVFC.RazorRender.Playground.Api.Parameters;

public sealed class WelcomeEmailParameters : IRazorCacheParameter
{
    public WelcomeEmailModel Model { get; set; } = default!;

    public string CacheKey => $"welcome-{Model.Email}";
}
