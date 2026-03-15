namespace MVFC.RazorRender.Tests;

public sealed class AppHostTests(AppHostFixture fixture) : IClassFixture<AppHostFixture>
{
    private readonly AppHostFixture _fixture = fixture;

    [Fact]
    public async Task WelcomeEmail_ShouldReturnOk()
    {
        // Arrange & Act
        using var response = await _fixture.PlaygroundApi.GetWelcomeAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.ContentHeaders!.ContentType!.MediaType.Should().Be("text/html");
        response.Content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task WelcomeEmail_ShouldContainName()
    {
        // Arrange & Act
        using var response = await _fixture.PlaygroundApi.GetWelcomeAsync("marcus@example.com");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().Contain("Marcus");
    }

    [Fact]
    public async Task WelcomeEmail_SecondCall_ShouldReturnSameHtml()
    {
        // Arrange & Act
        using var first = await _fixture.PlaygroundApi.GetWelcomeAsync("cache@example.com");
        using var second = await _fixture.PlaygroundApi.GetWelcomeAsync("cache@example.com");

        // Assert
        first.StatusCode.Should().Be(HttpStatusCode.OK);
        first.Content.Should().Be(second.Content);
    }

    [Fact]
    public async Task InvoiceEmail_ShouldReturnOk()
    {
        // Arrange & Act
        using var response = await _fixture.PlaygroundApi.GetInvoiceAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.ContentHeaders!.ContentType!.MediaType.Should().Be("text/html");
        response.Content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task InvoiceEmail_ShouldContainCustomerAndInvoiceNumber()
    {
        // Arrange & Act
        using var response = await _fixture.PlaygroundApi.GetInvoiceAsync("NF-2026-001");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().Contain("Acme Corp");
        response.Content.Should().Contain("NF-2026-001");
    }

    [Fact]
    public async Task InvoiceEmail_AsString_ShouldReturnJson()
    {
        // Arrange & Act
        using var response = await _fixture.PlaygroundApi.GetInvoiceAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task InvoiceEmail_DifferentNumbers_ShouldReturnDifferentHtml()
    {
        // Arrange & Act
        using var response1 = await _fixture.PlaygroundApi.GetInvoiceAsync("NF-2026-001");
        using var response2 = await _fixture.PlaygroundApi.GetInvoiceAsync("NF-2026-002");

        // Assert
        response1.Content.Should().NotBe(response2.Content);
    }
}
