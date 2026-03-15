var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();
builder.Services.AddRazorRenderCache(
    action: options =>
    {
        options.MaximumPayloadBytes = 1024 * 1024;
        options.DefaultEntryOptions = new()
        {
            Expiration = TimeSpan.FromMinutes(10),
        };
    },
    redisConnectionString: builder.Configuration.GetConnectionString("cache"));

var app = builder.Build();

app.MapRenderEndpoints();

await app.RunAsync().ConfigureAwait(false);
