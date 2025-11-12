namespace MVFC.RazorRender.Tests;

public class RazorRenderTest
{
    private readonly IServiceCollection _services;
    private readonly IRazorHtmlRenderService _razorRenderService;
    private readonly ICacheRazorHtmlRenderService _cacheRazorHtmlRender;

    public RazorRenderTest()
    {
        _services = new ServiceCollection();
        _services.AddRazorRenderCache(s => s.DefaultEntryOptions = MockFactory.MockCacheOptions());
        _razorRenderService = _services.BuildServiceProvider().GetRequiredService<IRazorHtmlRenderService>();
        _cacheRazorHtmlRender = _services.BuildServiceProvider().GetRequiredService<ICacheRazorHtmlRenderService>();
    }

    [Fact(DisplayName = "Renderização Razor HTML de Post")]
    public async Task Test_Post_RazorHtmlRender()
    {
        // Arrange
        var model = MockFactory.CreatePostMock();
        var parameters = new CommentParameterDto(Model: model, CacheKey: "test");

        // Act
        var html = await _razorRenderService.GenerateHtmlAsync<BlogView>(parameters);

        // Assert
        Assert.NotNull(html);
    }

    [Fact(DisplayName = "Renderização Razor HTML de Post com Cache")]
    public async Task Test_Post_CacheRazorHtmlRender()
    {
        // Arrange
        var model = MockFactory.CreatePostMock();
        var parameters = new CommentParameterDto(Model: model, CacheKey: "test");

        // Act
        var html = await _cacheRazorHtmlRender.GenerateHtmlAsync<BlogView>(parameters);

        // Assert
        Assert.NotNull(html);
    }
}